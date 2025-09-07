using App.Domain.Entities;

namespace App.Application.Common.Data;

public interface IEmailVerificationTokenRepository
{
  Task<EmailVerificationToken?> GetByTokenAsync(string token);
  Task<EmailVerificationToken?> GetRecentTokenByUserIdAsync(int userId, TimeSpan cooldown);
  Task InvalidateTokensByUserIdAsync(int userId);
  EmailVerificationToken Create(EmailVerificationToken emailVerificationToken);
  EmailVerificationToken Update(EmailVerificationToken token);
}