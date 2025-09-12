using App.Application.Auth.Configurations;
using App.Application.Auth.Events;
using App.Application.Auth.Services;
using App.Application.Common.Models.Email;

using Hangfire;

using MediatR;

using Microsoft.Extensions.Options;

namespace App.Application.Auth.EventHandlers;

public class PasswordResetRequestedEventHandler : INotificationHandler<PasswordResetRequestedEvent>
{
  private readonly IBackgroundJobClient _backgroundJobClient;
  private readonly AuthSettings _authSettings;

  public PasswordResetRequestedEventHandler(
    IBackgroundJobClient backgroundJobClient,
    IOptions<AuthSettings> authSettings)
  {
    _backgroundJobClient = backgroundJobClient;
    _authSettings = authSettings.Value;
  }

  public async Task Handle(PasswordResetRequestedEvent notification, CancellationToken cancellationToken)
  {
    var emailModel = new PasswordResetEmailModel(
      notification.Username,
      notification.Email,
      notification.ResetToken,
      DateTime.UtcNow.AddHours(_authSettings.PasswordResetTokenExpirationHours)
    );

    _backgroundJobClient.Enqueue<IAuthEmailService>(
      emailService => emailService.SendPasswordResetEmailAsync(emailModel, CancellationToken.None)
    );

    await Task.CompletedTask;
  }
}