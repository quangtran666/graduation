using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace App.Application.DI;

public static class Validation
{
  public static IServiceCollection AddValidationConfiguration(this IServiceCollection services)
  {
    services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
    return services;
  }
}