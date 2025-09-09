using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.Login;

public record LoginCommand(
  string UsernameOrEmail,
  string Password,
  bool RememberMe = false
) : IRequest<ErrorOr<LoginResult>>;