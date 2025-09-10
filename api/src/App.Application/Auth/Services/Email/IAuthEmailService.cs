using App.Application.Common.Models.Email;

using ErrorOr;

namespace App.Application.Auth.Services.Email;

public interface IAuthEmailService
{
  Task<ErrorOr<Success>> SendWelcomeEmailAsync(WelcomeEmailModel model, CancellationToken cancellationToken = default);
  Task<ErrorOr<Success>> SendPasswordResetEmailAsync(PasswordResetEmailModel model, CancellationToken cancellationToken = default);
  Task<ErrorOr<Success>> SendPasswordResetCompletedEmailAsync(PasswordResetCompletedEmailModel model, CancellationToken cancellationToken = default);
}