namespace App.Api.DI;

public static class Cors
{
  public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
  {
    var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

    services.AddCors(options =>
      options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins(allowedOrigins ?? [])
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials()));

    return services;
  }

  public static WebApplication UseCorsPolicy(this WebApplication app)
  {
    app.UseCors("CorsPolicy");
    return app;
  }
}