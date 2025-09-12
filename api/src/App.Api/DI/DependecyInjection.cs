using App.Infrastructure.DI;

namespace App.Api.DI;

public static class DependencyInjection
{
  public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddConfiguration(configuration);
    services.AddControllers();
    services.AddCorsPolicy(configuration);
    return services;
  }

  public static WebApplication UseApi(this WebApplication app)
  {
    app.UseHttpsRedirection();
    app.UseCorsPolicy();
    app.UseInfrastructure();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    return app;
  }
}