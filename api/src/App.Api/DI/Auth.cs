using App.Api.Authorization;
using App.Api.Authorization.Handlers;

using Microsoft.AspNetCore.Authorization;

namespace App.Api.DI;

public static class Auth
{
  public static IServiceCollection AddAuthServices(this IServiceCollection services)
  {
    services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();
    services.AddScoped<IAuthorizationHandler, RoleRequirementHandler>();
    services.AddScoped<IAuthorizationHandler, MultipleRolesRequirementHandler>();
    services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();

    return services;
  }
}