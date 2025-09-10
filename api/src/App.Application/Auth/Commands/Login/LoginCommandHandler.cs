using App.Application.Auth.Constants;
using App.Application.Auth.Services;
using App.Application.Common.Data;
using App.Application.Common.Models;
using App.Domain.Entities;
using App.Domain.Enums;

using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<LoginResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IPasswordService _passwordService;
  private readonly ITokenService _tokenService;
  private readonly IEmailVerificationService _emailVerificationService;

  public LoginCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordService passwordService,
    ITokenService tokenService,
    IEmailVerificationService emailVerificationService
  )
  {
    _unitOfWork = unitOfWork;
    _passwordService = passwordService;
    _tokenService = tokenService;
    _emailVerificationService = emailVerificationService;
  }

  public async Task<ErrorOr<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _unitOfWork.Users.GetByEmailOrUsernameAsync(request.UsernameOrEmail);
    if (user is null)
      return Error.NotFound(AuthErrors.User.NOT_FOUND, "Invalid credentials");

    if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
      return Error.Validation(AuthErrors.User.INVALID_CREDENTIALS, "Invalid credentials");

    if (!user.EmailVerified)
    {
      await _emailVerificationService.SendVerificationEmailAsync(user, cancellationToken);

      return Error.Forbidden(AuthErrors.User.EMAIL_NOT_VERIFIED_RESENT,
        "Please verify your email first. A new verification email has been sent if you haven't received one recently.");
    }

    if (user.Status == UserStatus.Banned)
      return Error.Forbidden(AuthErrors.User.ACCOUNT_BANNED, "Account has been permanently banned");

    if (user.Status == UserStatus.Suspended)
    {
      if (user.SuspendedUntil.HasValue && user.SuspendedUntil > DateTime.UtcNow)
      {
        var reason = !string.IsNullOrEmpty(user.SuspensionReason)
          ? $" Reason: {user.SuspensionReason}"
          : "";
        return Error.Forbidden(AuthErrors.User.ACCOUNT_SUSPENDED,
          $"Account suspended until {user.SuspendedUntil:yyyy-MM-dd HH:mm}.{reason}");
      }
      else
      {
        user.Status = UserStatus.Active;
        user.SuspendedUntil = null;
        user.SuspensionReason = null;
      }
    }

    var accessToken = _tokenService.GenerateAccessToken(user);
    var refreshToken = _tokenService.GenerateRefreshToken();

    // NOTE: Hiện tại chỉ khác nhau về thời gian expire (7 vs 30 ngày)
    // Cần implement: Session token vs Persistent token cho frontend
    // - Session token: expire khi đóng browser (không lưu persistent)
    // - Persistent token: lưu được lâu dài (lưu localStorage/cookie persistent)
    _unitOfWork.RefreshTokens.Create(new RefreshToken
    {
      UserId = user.Id,
      Token = refreshToken,
      ExpiresAt = DateTime.UtcNow.AddDays(request.RememberMe ? 30 : 7)
    });

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new LoginResult(
      Message: "Login successful",
      AccessToken: accessToken,
      RefreshToken: refreshToken,
      User: new UserData(user.Id, user.Username, user.Email, user.EmailVerified),
      IsRememberMe: request.RememberMe // Thông tin cho frontend xử lý storage phù hợp
    );
  }
}