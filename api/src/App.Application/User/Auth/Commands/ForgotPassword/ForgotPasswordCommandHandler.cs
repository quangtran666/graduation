using App.Application.Common.Data;
using App.Application.User.Auth.Constants;
using App.Application.User.Auth.Services;

using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ErrorOr<ForgotPasswordResult>>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IPasswordResetService _passwordResetService;

  public ForgotPasswordCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordResetService passwordResetService)
  {
    _unitOfWork = unitOfWork;
    _passwordResetService = passwordResetService;
  }

  public async Task<ErrorOr<ForgotPasswordResult>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
  {
    var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
    if (user is null)
      return Error.NotFound(AuthErrors.User.NOT_FOUND, "User not found");

    var result = await _passwordResetService.SendPasswordResetEmailAsync(user, cancellationToken);

    return result.MatchFirst<ErrorOr<ForgotPasswordResult>>(
      success => new ForgotPasswordResult(
        Message: "Password reset email sent successfully."
      ),
      error => error
    );
  }
}