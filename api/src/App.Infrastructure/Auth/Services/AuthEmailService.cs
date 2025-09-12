using App.Application.Auth.Services;
using App.Application.Common.Configurations;
using App.Application.Common.Models.Email;

using ErrorOr;

using FluentEmail.Core;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.Infrastructure.Auth.Services;

public partial class AuthEmailService : IAuthEmailService
{
  private readonly IFluentEmail _fluentEmail;
  private readonly EmailSettings _emailSettings;
  private readonly ILogger<AuthEmailService> _logger;

  public AuthEmailService(IFluentEmail fluentEmail, IOptions<EmailSettings> emailSettings, ILogger<AuthEmailService> logger)
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
  private partial void LogWelcomeEmailSendFailure(string email, string username, string errorMessages);

  [LoggerMessage(
    Level = LogLevel.Error,
    Message = "Exception occurred while sending password reset email to {Email} for user {Username}"
  )]
  private partial void LogPasswordResetEmailSendException(Exception ex, string email, string username);

  [LoggerMessage(
    Level = LogLevel.Error,
    Message = "Failed to send password reset email to {Email} for user {Username}. Errors: {ErrorMessages}"
  )]
  private partial void LogPasswordResetEmailSendFailure(string email, string username, string errorMessages);

  [LoggerMessage(
    Level = LogLevel.Error,
    Message = "Exception occurred while sending password reset completed email to {Email} for user {Username}"
  )]
  private partial void LogPasswordResetCompletedEmailSendException(Exception ex, string email, string username);

  [LoggerMessage(
    Level = LogLevel.Error,
    Message = "Failed to send password reset completed email to {Email} for user {Username}. Errors: {ErrorMessages}"
  )]
  private partial void LogPasswordResetCompletedEmailSendFailure(string email, string username, string errorMessages);

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
        LogWelcomeEmailSendFailure(model.Email, model.Username, errorMessages);
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

  public async Task<ErrorOr<Success>> SendPasswordResetEmailAsync(PasswordResetEmailModel model, CancellationToken cancellationToken = default)
  {
    try
    {
      var resetUrl = $"{_emailSettings.BaseUrl}/reset-password?token={model.ResetToken}";

      var result = await _fluentEmail
        .To(model.Email, model.Username)
        .Subject("Password Reset Request")
        .UsingTemplateFromFile(Path.Combine(AppContext.BaseDirectory, "Templates", "Email", "PasswordResetEmail.cshtml"), new
        {
          model.Username,
          ResetUrl = resetUrl,
          model.ExpiresAt
        })
        .SendAsync(cancellationToken);

      if (!result.Successful)
      {
        var errorMessages = string.Join(", ", result.ErrorMessages);
        LogPasswordResetEmailSendFailure(model.Email, model.Username, errorMessages);
        throw new InvalidOperationException($"Failed to send password reset email to {model.Email}");
      }

      return Result.Success;
    }
    catch (Exception ex) when (ex is not OperationCanceledException)
    {
      LogPasswordResetEmailSendException(ex, model.Email, model.Username);
      throw;
    }
  }

  public async Task<ErrorOr<Success>> SendPasswordResetCompletedEmailAsync(PasswordResetCompletedEmailModel model, CancellationToken cancellationToken = default)
  {
    try
    {
      var result = await _fluentEmail
        .To(model.Email, model.Username)
        .Subject("Password Reset Successful")
        .UsingTemplateFromFile(Path.Combine(AppContext.BaseDirectory, "Templates", "Email", "PasswordResetCompletedEmail.cshtml"), new
        {
          model.Username
        })
        .SendAsync(cancellationToken);

      if (!result.Successful)
      {
        var errorMessages = string.Join(", ", result.ErrorMessages);
        LogPasswordResetCompletedEmailSendFailure(model.Email, model.Username, errorMessages);
        throw new InvalidOperationException($"Failed to send password reset completed email to {model.Email}");
      }

      return Result.Success;
    }
    catch (Exception ex) when (ex is not OperationCanceledException)
    {
      LogPasswordResetCompletedEmailSendException(ex, model.Email, model.Username);
      throw;
    }
  }
}