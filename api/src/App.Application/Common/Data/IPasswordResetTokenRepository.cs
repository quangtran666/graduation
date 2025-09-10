using App.Domain.Entities;

namespace App.Application.Common.Data;

public interface IPasswordResetTokenRepository
{
  Task<PasswordResetToken?> GetByTokenAsync(string token);
  Task<PasswordResetToken?> GetRecentTokenByUserIdAsync(int userId, TimeSpan cooldown);
  Task InvalidateTokensByUserIdAsync(int userId);
  PasswordResetToken Create(PasswordResetToken passwordResetToken);
  PasswordResetToken Update(PasswordResetToken token);
}