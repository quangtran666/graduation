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
    return services;
  }
}