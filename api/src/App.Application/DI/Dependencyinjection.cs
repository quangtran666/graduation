using App.Application.Auth.Services;

using Microsoft.Extensions.DependencyInjection;

namespace App.Application.DI;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR();
    services.AddValidationConfiguration();
    services.AddApplicationServices();
    return services;
  }

  private static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IEmailVerificationService, EmailVerificationService>();
    return services;
  }
}