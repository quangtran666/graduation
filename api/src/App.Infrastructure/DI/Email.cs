using System.Net;
using System.Net.Mail;

using App.Application.Auth.Services;
using App.Application.Common.Configurations;
using App.Infrastructure.Auth.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.DI;

public static class Email
{
  public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
    var emailSettings = configuration.GetSection(EmailSettings.SectionName).Get<EmailSettings>();

    services.AddFluentEmail(emailSettings!.From.Address, emailSettings.From.Name)
      .AddRazorRenderer()
      .AddSmtpSender(() => new SmtpClient(emailSettings.Smtp.Host, emailSettings.Smtp.Port)
      {
        EnableSsl = emailSettings.Smtp.EnableSsl,
        Credentials = string.IsNullOrEmpty(emailSettings.Smtp.Username)
            ? null
            : new NetworkCredential(emailSettings.Smtp.Username, emailSettings.Smtp.Password)
      });

    services.AddScoped<IAuthEmailService, AuthEmailService>();

    return services;
  }
}