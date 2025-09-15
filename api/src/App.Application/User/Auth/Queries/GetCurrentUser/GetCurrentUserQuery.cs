using ErrorOr;

using MediatR;

namespace App.Application.User.Auth.Queries.GetCurrentUser;

public record GetCurrentUserQuery() : IRequest<ErrorOr<GetCurrentUserResult>>;