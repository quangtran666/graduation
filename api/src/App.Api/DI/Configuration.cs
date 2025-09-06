using App.Application.Auth.Configurations;

namespace App.Api.DI;

public static class Configuration
{
  public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<RouteOptions>(options =>
    {
      options.LowercaseUrls = true;
      options.LowercaseQueryStrings = true;
    });
    services.Configure<AuthSettings>(configuration.GetSection(AuthSettings.SectionName));
    return services;
  }
}