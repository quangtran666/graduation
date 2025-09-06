using App.Application.Common.Data;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.DI;

public static class Persistence
{
  public static IServiceCollection AddPersistence(
    this IServiceCollection services
  )
  {
    services.AddDbContext<AppDbContext>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    services.AddScoped<IEmailVerificationTokenRepository, EmailVerificationTokenRepository>();
    return services;
  }
}