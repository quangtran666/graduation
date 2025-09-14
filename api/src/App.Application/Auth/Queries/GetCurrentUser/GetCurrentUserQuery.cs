using ErrorOr;

using MediatR;

namespace App.Application.Auth.Queries.GetCurrentUser;

public record GetCurrentUserQuery() : IRequest<ErrorOr<GetCurrentUserResult>>;