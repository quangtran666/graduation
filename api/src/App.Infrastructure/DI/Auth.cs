using System.Text;

using App.Application.Auth.Configurations;
using App.Application.Auth.Services;
using App.Infrastructure.Auth.Events;
using App.Infrastructure.Auth.Services;
using App.Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.Infrastructure.DI;

public static class Auth
{
  public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    services.Configure<AuthSettings>(configuration.GetSection(AuthSettings.SectionName));
    services.Configure<AuthCookieSettings>(configuration.GetSection(AuthCookieSettings.Section));
    services.ConfigureOptions<PostConfigureAuthCookieSettings>();

    var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();

    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.SecretKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };

      options.EventsType = typeof(JwtEvents);
    });
    services.AddAuthorization();
    services.AddScoped<JwtEvents>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IPasswordService, PasswordService>();
    services.AddScoped<IPasswordResetService, PasswordResetService>();
    services.AddScoped<IAuthCookieService, AuthCookieService>();
    services.AddScoped<ICurrentUserService, CurrentUserService>();
    services.AddHttpContextAccessor();
    return services;
  }
}