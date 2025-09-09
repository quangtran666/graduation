using App.Application.Auth.Constants;
using App.Application.Common.Data;

using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ErrorOr<string>>
{
  private readonly IUnitOfWork _unitOfWork;

  public LogoutCommandHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<ErrorOr<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
  {
    var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(request.RefreshToken);
    if (refreshToken != null && !refreshToken.IsRevoked)
    {
      refreshToken.IsRevoked = true;
      await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    return "Logged out successfully";
  }
}