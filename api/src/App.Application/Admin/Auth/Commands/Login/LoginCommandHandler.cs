using App.Application.Admin.Auth.Constants;
using App.Application.Common.Constants;
using App.Application.Common.Data;
using App.Application.Common.Models;
using App.Application.User.Auth.Configurations;
using App.Application.User.Auth.Services;
using App.Domain.Enums;

using ErrorOr;

using MediatR;

using Microsoft.Extensions.Options;

using RefreshTokenEntity = App.Domain.Entities.RefreshToken;

namespace App.Application.Admin.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<LoginResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IPasswordService _passwordService;
  private readonly ITokenService _tokenService;
  private readonly IAuthCookieService _authCookieService;
  private readonly AuthSettings _authSettings;

  public LoginCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordService passwordService,
    ITokenService tokenService,
    IAuthCookieService authCookieService,
    IOptions<AuthSettings> authSettings
  )
  {
    _unitOfWork = unitOfWork;
    _passwordService = passwordService;
    _tokenService = tokenService;
    _authCookieService = authCookieService;
    _authSettings = authSettings.Value;
  }

  public async Task<ErrorOr<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _unitOfWork.Users.GetByEmailWithRolesAsync(request.Email);

    if (user is null)
      return Error.NotFound(AdminAuthErrors.User.NOT_FOUND, "Invalid credentials");

    if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
      return Error.Validation(AdminAuthErrors.User.INVALID_CREDENTIALS, "Invalid credentials");

    if (user.Status == UserStatus.Banned)
      return Error.Forbidden(AdminAuthErrors.User.ACCOUNT_BANNED, "Account has been permanently banned");

    if (user.Status == UserStatus.Suspended)
    {
      if (user.SuspendedUntil.HasValue && user.SuspendedUntil > DateTime.UtcNow)
      {
        var reason = !string.IsNullOrEmpty(user.SuspensionReason)
          ? $" Reason: {user.SuspensionReason}"
          : "";
        return Error.Forbidden(AdminAuthErrors.User.ACCOUNT_SUSPENDED,
          $"Account suspended until {user.SuspendedUntil:yyyy-MM-dd HH:mm}.{reason}");
      }
      else
      {
        user.Status = UserStatus.Active;
        user.SuspendedUntil = null;
        user.SuspensionReason = null;
      }
    }

    var hasAdminRole = user.UserRoles.Any(ur => ur.Role.Name == RoleConstants.ADMIN);
    if (!hasAdminRole)
      return Error.Forbidden(AdminAuthErrors.User.NOT_ADMIN, "Access denied. Admin privileges required.");

    var accessToken = _tokenService.GenerateAccessToken(user);
    var refreshToken = _tokenService.GenerateRefreshToken();

    _unitOfWork.RefreshTokens.Create(new RefreshTokenEntity
    {
      UserId = user.Id,
      Token = refreshToken,
      ExpiresAt = DateTime.UtcNow.AddDays(_authSettings.RefreshTokenExpirationDays)
    });

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _authCookieService.SetAccessTokenCookie(accessToken);
    _authCookieService.SetRefreshTokenCookie(refreshToken);

    return new LoginResult(
      Message: "Admin login successful",
      User: new UserData(user.Id, user.Username, user.Email, user.EmailVerified)
    );
  }
}