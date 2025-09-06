using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.DI;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    services.AddPersistence();
    services.AddAuthServices(configuration);
    services.AddEmail(configuration);
    services.AddBackgroundJobs(configuration);
    return services;
  }

  public static WebApplication UseInfrastructure(this WebApplication app)
  {
    app.UseBackgroundJobs();
    return app;
  }
}