using Microsoft.AspNetCore.Authorization;

namespace App.Api.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class RequireRoleAttribute : AuthorizeAttribute
{
  public RequireRoleAttribute(string role)
  {
    Policy = $"Role:{role}";
  }
}