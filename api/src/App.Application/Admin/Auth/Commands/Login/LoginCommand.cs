using ErrorOr;

using MediatR;

namespace App.Application.Admin.Auth.Commands.Login;

public record LoginCommand(
  string Email,
  string Password
) : IRequest<ErrorOr<LoginResult>>;