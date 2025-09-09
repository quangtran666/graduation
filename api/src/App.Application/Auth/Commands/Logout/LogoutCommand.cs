using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.Logout;

public record LogoutCommand(
  string RefreshToken
) : IRequest<ErrorOr<string>>;