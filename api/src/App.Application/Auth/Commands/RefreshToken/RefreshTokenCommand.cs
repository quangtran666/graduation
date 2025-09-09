using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.RefreshTokens;

public record RefreshTokenCommand(
  string RefreshToken
) : IRequest<ErrorOr<RefreshTokenResult>>;