

using App.Application.Common.Data;
using App.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Repositories;

public class EmailVerificationTokenRepository : IEmailVerificationTokenRepository
{
  private readonly AppDbContext _context;

  public EmailVerificationTokenRepository(AppDbContext context)
  {
    _context = context;
  }

  public EmailVerificationToken Create(EmailVerificationToken emailVerificationToken)
  {
    _context.EmailVerificationTokens.Add(emailVerificationToken);
    return emailVerificationToken;
  }

  public async Task<EmailVerificationToken?> GetByTokenAsync(string token)
  {
    return await _context.EmailVerificationTokens
      .Include(x => x.User)
      .FirstOrDefaultAsync(x => x.Token == token && x.ExpiresAt > DateTime.UtcNow && x.UsedAt == null);
  }

  public EmailVerificationToken Update(EmailVerificationToken token)
  {
    _context.EmailVerificationTokens.Update(token);
    return token;
  }
}