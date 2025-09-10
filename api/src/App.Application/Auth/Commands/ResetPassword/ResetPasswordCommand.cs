using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.ResetPassword;

public record ResetPasswordCommand(
  string Token,
  string NewPassword,
  string ConfirmPassword
) : IRequest<ErrorOr<ResetPasswordResult>>;