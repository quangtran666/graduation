using App.Application.Auth.Constants;
using App.Application.Auth.Services;
using App.Application.Common.Data;
using App.Domain.Entities;
using App.Domain.Enums;

using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.RefreshTokens;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<RefreshTokenResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ITokenService _tokenService;

  public RefreshTokenCommandHandler(
    IUnitOfWork unitOfWork,
    ITokenService tokenService
  )
  {
    _unitOfWork = unitOfWork;
    _tokenService = tokenService;
  }

  public async Task<ErrorOr<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
  {
    var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(request.RefreshToken);
    if (refreshToken == null || refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTime.UtcNow)
      return Error.Validation(AuthErrors.Token.INVALID_TOKEN, "Invalid or expired refresh token");

    var user = await _unitOfWork.Users.GetByIdAsync(refreshToken.UserId);
    if (user == null || !user.EmailVerified || user.Status != UserStatus.Active)
      return Error.Forbidden(AuthErrors.User.ACCOUNT_SUSPENDED, "User account is not active");

    refreshToken.IsRevoked = true;

    var newAccessToken = _tokenService.GenerateAccessToken(user);
    var newRefreshToken = _tokenService.GenerateRefreshToken();

    _unitOfWork.RefreshTokens.Create(new RefreshToken
    {
      UserId = user.Id,
      Token = newRefreshToken,
      ExpiresAt = DateTime.UtcNow.AddDays(30)
    });

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new RefreshTokenResult(newAccessToken, newRefreshToken);
  }
}