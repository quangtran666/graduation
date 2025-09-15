using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<ErrorOr<RefreshTokenResult>> { }