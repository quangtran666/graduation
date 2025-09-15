using App.Application.Common.Models.Email;
using App.Application.User.Auth.Configurations;
using App.Application.User.Auth.Events;
using App.Application.User.Auth.Services;

using Hangfire;

using MediatR;

using Microsoft.Extensions.Options;

namespace App.Application.User.Auth.EventHandlers;

public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
{
  private readonly IBackgroundJobClient _backgroundJobClient;
  private readonly AuthSettings _authSettings;

  public UserRegisteredEventHandler(IBackgroundJobClient backgroundJobClient, IOptions<AuthSettings> authSettings)
  {
    _backgroundJobClient = backgroundJobClient;
    _authSettings = authSettings.Value;
  }

  public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
  {
    var emailModel = new WelcomeEmailModel(
      notification.Username,
      notification.Email,
      notification.VerificationToken,
      DateTime.UtcNow.AddMinutes(_authSettings.EmailVerificationTokenExpirationMinutes)
    );

    _backgroundJobClient.Enqueue<IAuthEmailService>(
      emailService => emailService.SendWelcomeEmailAsync(emailModel, CancellationToken.None)
    );

    await Task.CompletedTask;
  }
}