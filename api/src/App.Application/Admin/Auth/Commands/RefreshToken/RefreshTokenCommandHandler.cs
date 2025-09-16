using App.Application.Auth.Services;
using App.Application.Common.Data;
using App.Application.User.Auth.Configurations;
using App.Application.User.Auth.Constants;
using App.Domain.Enums;

using ErrorOr;

using MediatR;

using Microsoft.Extensions.Options;

using RefreshTokenEntity = App.Domain.Entities.RefreshToken;

namespace App.Application.Admin.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<RefreshTokenResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ITokenService _tokenService;
  private readonly IAuthCookieService _authCookieService;
  private readonly AuthSettings _authSettings;

  public RefreshTokenCommandHandler(
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IAuthCookieService authCookieService,
    IOptions<AuthSettings> authSettings
  )
  {
    _unitOfWork = unitOfWork;
    _tokenService = tokenService;
    _authCookieService = authCookieService;
    _authSettings = authSettings.Value;
  }

  public async Task<ErrorOr<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
  {
    var refreshTokenValue = _authCookieService.GetRefreshTokenFromCookie();
    if (string.IsNullOrEmpty(refreshTokenValue))
      return Error.Validation(AuthErrors.Token.INVALID_TOKEN, "Refresh token not found in cookies");

    var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshTokenValue);
    if (refreshToken == null || refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTime.UtcNow)
      return Error.Validation(AuthErrors.Token.INVALID_TOKEN, "Invalid or expired refresh token");

    var user = await _unitOfWork.Users.GetByIdAsync(refreshToken.UserId);
    if (user == null || !user.EmailVerified || user.Status != UserStatus.Active)
      return Error.Forbidden(AuthErrors.User.ACCOUNT_SUSPENDED, "Admin account is not active");

    refreshToken.IsRevoked = true;

    var newAccessToken = _tokenService.GenerateAccessToken(user);
    var newRefreshToken = _tokenService.GenerateRefreshToken();

    _unitOfWork.RefreshTokens.Create(new RefreshTokenEntity
    {
      UserId = user.Id,
      Token = newRefreshToken,
      ExpiresAt = DateTime.UtcNow.AddDays(_authSettings.RefreshTokenExpirationDays),
    });

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    _authCookieService.SetAccessTokenCookie(newAccessToken);
    _authCookieService.SetRefreshTokenCookie(newRefreshToken);

    return new RefreshTokenResult("Tokens refreshed successfully");
  }
}