using ErrorOr;

using MediatR;

namespace App.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<ErrorOr<RefreshTokenResult>> { }