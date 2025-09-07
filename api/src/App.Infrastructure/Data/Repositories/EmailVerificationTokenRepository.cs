

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

  public async Task<EmailVerificationToken?> GetRecentTokenByUserIdAsync(int userId, TimeSpan cooldown)
  {
    var cutoffTime = DateTime.UtcNow.Subtract(cooldown);

    return await _context.EmailVerificationTokens
      .Where(t => t.UserId == userId
                  && t.CreatedAt >= cutoffTime
                  && t.UsedAt == null)
      .OrderByDescending(t => t.CreatedAt)
      .FirstOrDefaultAsync();
  }

  public async Task InvalidateTokensByUserIdAsync(int userId)
  {
    var tokens = await _context.EmailVerificationTokens
      .Where(t => t.UserId == userId && t.UsedAt == null)
      .ToListAsync();

    foreach (var token in tokens)
    {
      token.UsedAt = DateTime.UtcNow;
    }
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