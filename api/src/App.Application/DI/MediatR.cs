using App.Application.Common.Behaviors;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace App.Application.DI;

public static class MediatR
{
  public static IServiceCollection AddMediatR(this IServiceCollection services)
  {
    services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    return services;
  }
}