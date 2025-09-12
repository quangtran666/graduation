using App.Application.Auth.Services;
using App.Application.Common.Data;

using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ErrorOr<string>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IAuthCookieService _authCookieService;

  public LogoutCommandHandler(IUnitOfWork unitOfWork, IAuthCookieService authCookieService)
  {
    _unitOfWork = unitOfWork;
    _authCookieService = authCookieService;
  }

  public async Task<ErrorOr<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
  {
    var refreshTokenValue = _authCookieService.GetRefreshTokenFromCookie();

    if (!string.IsNullOrEmpty(refreshTokenValue))
    {
      var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshTokenValue);
      if (refreshToken != null && !refreshToken.IsRevoked)
      {
        refreshToken.IsRevoked = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
      }
    }

    _authCookieService.RemoveAuthCookies();

    return "Logged out successfully";
  }
}