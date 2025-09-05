using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.DI;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services
  )
  {
    services.AddPersistence();
    return services;
  }
}