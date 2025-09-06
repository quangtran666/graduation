using Hangfire;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.DI;

public static class BackgroundJobs
{
  public static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("HangfireConnection");

    services.AddHangfire(config => config
      .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
      .UseSimpleAssemblyNameTypeSerializer()
      .UseRecommendedSerializerSettings()
      .UseSqlServerStorage(connectionString)
    );

    services.AddHangfireServer();

    return services;
  }

  public static WebApplication UseBackgroundJobs(this WebApplication app)
  {
    app.UseHangfireDashboard("/hangfire");
    return app;
  }
}