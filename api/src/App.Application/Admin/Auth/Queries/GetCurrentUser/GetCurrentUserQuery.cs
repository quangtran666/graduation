using ErrorOr;

using MediatR;

namespace App.Application.Admin.Auth.Queries.GetCurrentUser;

public record GetCurrentUserQuery() : IRequest<ErrorOr<GetCurrentUserResult>>;