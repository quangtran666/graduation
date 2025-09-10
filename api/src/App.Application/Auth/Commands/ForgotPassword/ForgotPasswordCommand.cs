using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.ForgotPassword;

public record ForgotPasswordCommand(
  string Email
) : IRequest<ErrorOr<ForgotPasswordResult>>;