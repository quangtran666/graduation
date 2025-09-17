using Microsoft.AspNetCore.Authorization;

namespace App.Api.Authorization.Requirements;

public class MultipleRolesRequirement : IAuthorizationRequirement
{
  public string[] Roles { get; }

  public MultipleRolesRequirement(params string[] roles)
  {
    Roles = roles;
  }
}