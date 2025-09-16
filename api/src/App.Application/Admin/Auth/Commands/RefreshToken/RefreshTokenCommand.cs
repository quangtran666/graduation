using ErrorOr;

using MediatR;

namespace App.Application.Admin.Auth.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<ErrorOr<RefreshTokenResult>> { }