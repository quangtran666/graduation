using App.Application.Auth.Services;
using App.Application.Common.Models;

using ErrorOr;

using MediatR;

namespace App.Application.Admin.Auth.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ErrorOr<GetCurrentUserResult>>
{
  private readonly ICurrentUserService _currentUserService;

  public GetCurrentUserQueryHandler(ICurrentUserService currentUserService)
  {
    _currentUserService = currentUserService;
  }

  public Task<ErrorOr<GetCurrentUserResult>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
  {
    var userIdResult = _currentUserService.GetCurrentUserId();
    if (userIdResult.IsError)
      return Task.FromResult<ErrorOr<GetCurrentUserResult>>(userIdResult.Errors);

    var emailResult = _currentUserService.GetCurrentUserEmail();
    if (emailResult.IsError)
      return Task.FromResult<ErrorOr<GetCurrentUserResult>>(emailResult.Errors);

    var usernameResult = _currentUserService.GetCurrentUsername();
    if (usernameResult.IsError)
      return Task.FromResult<ErrorOr<GetCurrentUserResult>>(usernameResult.Errors);

    var emailVerifiedResult = _currentUserService.IsEmailVerified();
    if (emailVerifiedResult.IsError)
      return Task.FromResult<ErrorOr<GetCurrentUserResult>>(emailVerifiedResult.Errors);

    var result = new GetCurrentUserResult(
      Message: "Admin information retrieved successfully",
      User: new UserData(
        userIdResult.Value,
        usernameResult.Value,
        emailResult.Value,
        emailVerifiedResult.Value
      )
    );

    return Task.FromResult<ErrorOr<GetCurrentUserResult>>(result);
  }
}