using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.Login;

public record LoginCommand(
  string UsernameOrEmail,
  string Password
) : IRequest<ErrorOr<LoginResult>>;