namespace App.Api.DI;

public static class DependencyInjection
{
  public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddConfiguration(configuration);
    services.AddControllers();
    return services;
  }
}