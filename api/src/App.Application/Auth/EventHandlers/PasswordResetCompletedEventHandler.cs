using App.Application.Auth.Events;
using App.Application.Auth.Services;
using App.Application.Common.Models.Email;

using Hangfire;

using MediatR;

namespace App.Application.Auth.EventHandlers;

public class PasswordResetCompletedEventHandler : INotificationHandler<PasswordResetCompletedEvent>
{
  private readonly IBackgroundJobClient _backgroundJobClient;

  public PasswordResetCompletedEventHandler(IBackgroundJobClient backgroundJobClient)
  {
    _backgroundJobClient = backgroundJobClient;
  }

  public async Task Handle(PasswordResetCompletedEvent notification, CancellationToken cancellationToken)
  {
    var emailModel = new PasswordResetCompletedEmailModel(
      notification.Username,
      notification.Email
    );

    _backgroundJobClient.Enqueue<IAuthEmailService>(
      emailService => emailService.SendPasswordResetCompletedEmailAsync(emailModel, CancellationToken.None)
    );

    await Task.CompletedTask;
  }
}