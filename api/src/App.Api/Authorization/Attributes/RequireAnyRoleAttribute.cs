using Microsoft.AspNetCore.Authorization;

namespace App.Api.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class RequireAnyRoleAttribute : AuthorizeAttribute
{
  public RequireAnyRoleAttribute(params string[] roles)
  {
    Policy = $"Roles:{string.Join(",", roles)}";
  }
}