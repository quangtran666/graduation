using App.Application.Auth.Services;
using App.Infrastructure.Auth.Configurations;
using App.Infrastructure.Auth.Services;

using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.DI;

public static class Auth
{
  public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IPasswordService, PasswordService>();
    return services;
  }
}