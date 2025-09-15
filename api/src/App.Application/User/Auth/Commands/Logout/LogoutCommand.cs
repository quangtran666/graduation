using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Commands.Logout;

public record LogoutCommand : IRequest<ErrorOr<string>> { }