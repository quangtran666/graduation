using Microsoft.Extensions.DependencyInjection;

namespace App.Application.DI;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR();
    services.AddValidationConfiguration();
    return services;
  }
}