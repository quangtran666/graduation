using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Commands.Login;

public record LoginCommand(
  string UsernameOrEmail,
  string Password
) : IRequest<ErrorOr<LoginResult>>;