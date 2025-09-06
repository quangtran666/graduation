using App.Application.Common.Models.Email;

using ErrorOr;

namespace App.Application.Common.Services;

public interface IEmailService
{
  Task<ErrorOr<Success>> SendWelcomeEmailAsync(WelcomeEmailModel model, CancellationToken cancellationToken = default);
}