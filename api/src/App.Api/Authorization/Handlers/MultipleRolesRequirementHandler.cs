using App.Api.Authorization.Requirements;
using App.Application.Auth.Services;
using App.Application.Common.Data;

using Microsoft.AspNetCore.Authorization;

namespace App.Api.Authorization.Handlers;

public class MultipleRolesRequirementHandler : AuthorizationHandler<MultipleRolesRequirement>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ICurrentUserService _currentUserService;

  public MultipleRolesRequirementHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
  {
    _unitOfWork = unitOfWork;
    _currentUserService = currentUserService;
  }

  protected override async Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    MultipleRolesRequirement requirement)
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

    var userRoles = user.UserRoles.Select(ur => ur.Role.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
    var hasAnyRequiredRole = requirement.Roles.All(userRoles.Contains);

    if (hasAnyRequiredRole)
      context.Succeed(requirement);
    else
      context.Fail();
  }
}