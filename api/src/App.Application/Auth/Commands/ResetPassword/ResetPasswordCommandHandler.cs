using App.Application.Auth.Services;

using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<ResetPasswordResult>>
{
  private readonly IPasswordResetService _passwordResetService;

  public ResetPasswordCommandHandler(IPasswordResetService passwordResetService)
  {
    _passwordResetService = passwordResetService;
  }

  public async Task<ErrorOr<ResetPasswordResult>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
  {
    var result = await _passwordResetService.ResetPasswordAsync(request.Token, request.NewPassword, cancellationToken);

    return result.MatchFirst<ErrorOr<ResetPasswordResult>>(
      success => new ResetPasswordResult(
        Message: "Password reset successfully."
      ),
      error => error
    );
  }
}