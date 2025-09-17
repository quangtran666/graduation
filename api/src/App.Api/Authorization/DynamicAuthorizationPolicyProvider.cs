using App.Api.Authorization.Requirements;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace App.Api.Authorization;

public class DynamicAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
  private const string ROLE_POLICY_PREFIX = "Role:";
  private const string PERMISSION_POLICY_PREFIX = "Permission:";
  private const string MULTIPLE_ROLES_PREFIX = "Roles:";

  public DynamicAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    : base(options) { }

  public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
  {
    // Check if policy already exists
    var existingPolicy = await base.GetPolicyAsync(policyName);
    if (existingPolicy != null)
      return existingPolicy;

    // Generate policy on-the-fly based on naming convention
    if (policyName.StartsWith(ROLE_POLICY_PREFIX, StringComparison.Ordinal))
    {
      var roleName = policyName[ROLE_POLICY_PREFIX.Length..];
      return new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddRequirements(new RoleRequirement(roleName))
        .Build();
    }

    if (policyName.StartsWith(PERMISSION_POLICY_PREFIX, StringComparison.Ordinal))
    {
      var permissionName = policyName[PERMISSION_POLICY_PREFIX.Length..];
      return new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddRequirements(new PermissionRequirement(permissionName))
        .Build();
    }

    if (policyName.StartsWith(MULTIPLE_ROLES_PREFIX, StringComparison.Ordinal))
    {
      var rolesString = policyName[MULTIPLE_ROLES_PREFIX.Length..];
      var roles = rolesString.Split(',', StringSplitOptions.RemoveEmptyEntries)
        .Select(role => role.Trim())
        .ToArray();

      return new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddRequirements(new MultipleRolesRequirement(roles))
        .Build();
    }

    return null;
  }
}