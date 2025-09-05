using App.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.DI;

public static class Persistence
{
  public static IServiceCollection AddPersistence(
    this IServiceCollection services
  )
  {
    services.AddDbContext<AppDbContext>();
    return services;
  }
}