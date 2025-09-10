using App.Domain.Entities;

using ErrorOr;

namespace App.Application.Auth.Services;

public interface IPasswordResetService
{
  Task<ErrorOr<Success>> SendPasswordResetEmailAsync(User user, CancellationToken cancellationToken = default);
  Task<ErrorOr<Success>> ResetPasswordAsync(string token, string newPassword, CancellationToken cancellationToken = default);
}