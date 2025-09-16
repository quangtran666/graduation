using ErrorOr;

using MediatR;

namespace App.Application.Admin.Auth.Commands.Logout;

public record LogoutCommand : IRequest<ErrorOr<string>> { }