using ErrorOr;

using UserDomain = App.Domain.Entities.User;

namespace App.Application.User.Auth.Services;

public interface IPasswordResetService
{
  Task<ErrorOr<Success>> SendPasswordResetEmailAsync(UserDomain user, CancellationToken cancellationToken = default);
  Task<ErrorOr<Success>> ResetPasswordAsync(string token, string newPassword, CancellationToken cancellationToken = default);
}