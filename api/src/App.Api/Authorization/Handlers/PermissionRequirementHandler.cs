using App.Api.Authorization.Requirements;
using App.Application.Auth.Services;
using App.Application.Common.Data;

using Microsoft.AspNetCore.Authorization;

namespace App.Api.Authorization.Handlers;

// Todo: When i start implementing the permission feature, remember to check this agian
public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly ICurrentUserService _currentUserService;

  public PermissionRequirementHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
  {
    _unitOfWork = unitOfWork;
    _currentUserService = currentUserService;
  }

  protected override async Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    PermissionRequirement requirement)
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

    var hasPermission = user.UserRoles
      .SelectMany(ur => ur.Role.RolePermissions)
      .Any(rp => rp.Permission.Name.Equals(requirement.Permission, StringComparison.OrdinalIgnoreCase));

    if (hasPermission)
      context.Succeed(requirement);
    else
      context.Fail();
  }
}