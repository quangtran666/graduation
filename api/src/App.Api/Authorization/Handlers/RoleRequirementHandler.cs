using App.Api.Authorization.Requirements;
using App.Application.Auth.Services;
using App.Application.Common.Constants;
using App.Application.Common.Data;

using Microsoft.AspNetCore.Authorization;

namespace App.Api.Authorization.Handlers;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ICurrentUserService _currentUserService;

  public RoleRequirementHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
  {
    _unitOfWork = unitOfWork;
    _currentUserService = currentUserService;
  }

  protected override async Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    RoleRequirement requirement)
  {
    var userEmailResult = _currentUserService.GetCurrentUserEmail();
    if (userEmailResult.IsError)
    {
      context.Fail();
      return;
    }

    var user = await _unitOfWork.Users.GetByEmailWithRolesAsync(userEmailResult.Value);
    if (user == null)
    {
      context.Fail();
      return;
    }

    var hasRole = user.UserRoles.Any(ur =>
      ur.Role.Name.Equals(requirement.Role, StringComparison.OrdinalIgnoreCase));

    if (hasRole)
      context.Succeed(requirement);
    else
      context.Fail();
  }
}