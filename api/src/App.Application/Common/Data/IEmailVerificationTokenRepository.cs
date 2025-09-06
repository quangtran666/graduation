using App.Domain.Entities;

namespace App.Application.Common.Data;

public interface IEmailVerificationTokenRepository
{
  EmailVerificationToken Create(EmailVerificationToken emailVerificationToken);
  Task<EmailVerificationToken?> GetByTokenAsync(string token);
  EmailVerificationToken Update(EmailVerificationToken token);
}