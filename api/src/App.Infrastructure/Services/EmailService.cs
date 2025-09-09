
using App.Application.Common.Configurations;
using App.Application.Common.Models.Email;
using App.Application.Common.Services;

using ErrorOr;

using FluentEmail.Core;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.Infrastructure.Services;

public partial class EmailService : IEmailService
{
  private readonly IFluentEmail _fluentEmail;
  private readonly EmailSettings _emailSettings;
  private readonly ILogger<EmailService> _logger;

  public EmailService(IFluentEmail fluentEmail, IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
  {
    _fluentEmail = fluentEmail;
    _emailSettings = emailSettings.Value;
    _logger = logger;
  }

  [LoggerMessage(
    Level = LogLevel.Error,
    Message = "Exception occurred while sending email to {Email} for user {Username}"
  )]
  private partial void LogEmailSendException(Exception ex, string email, string username);

  [LoggerMessage(
    Level = LogLevel.Error,
    Message = "Failed to send welcome email to {Email} for user {Username}. Errors: {ErrorMessages}"
  )]
  private partial void LogEmailSendFailure(string email, string username, string errorMessages);

  public async Task<ErrorOr<Success>> SendWelcomeEmailAsync(WelcomeEmailModel model, CancellationToken cancellationToken = default)
  {
    try
    {
      var verificationUrl = $"{_emailSettings.BaseUrl}/email-confirmed?token={model.VerificationToken}";

      var result = await _fluentEmail
        .To(model.Email, model.Username)
        .Subject("Welcome! Please verify your email")
        .UsingTemplateFromFile(Path.Combine(AppContext.BaseDirectory, "Templates", "Email", "WelcomeEmail.cshtml"), new
        {
          model.Username,
          VerificationUrl = verificationUrl,
          model.ExpiresAt
        })
        .SendAsync(cancellationToken);

      if (!result.Successful)
      {
        var errorMessages = string.Join(", ", result.ErrorMessages);
        LogEmailSendFailure(model.Email, model.Username, errorMessages);
        throw new InvalidOperationException($"Failed to send welcome email to {model.Email}");
      }

      return Result.Success;
    }
    catch (Exception ex) when (ex is not OperationCanceledException)
    {
      LogEmailSendException(ex, model.Email, model.Username);
      throw;
    }
  }
}