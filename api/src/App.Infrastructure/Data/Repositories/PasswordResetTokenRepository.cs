using App.Application.Common.Data;
using App.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Repositories;

public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
  private readonly AppDbContext _context;

  public PasswordResetTokenRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<PasswordResetToken?> GetByTokenAsync(string token)
  {
    return await _context.PasswordResetTokens
      .Include(x => x.User)
      .FirstOrDefaultAsync(x => x.Token == token);
  }

  public async Task<PasswordResetToken?> GetRecentTokenByUserIdAsync(int userId, TimeSpan cooldown)
  {
    var cutoffTime = DateTime.UtcNow.Subtract(cooldown);

    return await _context.PasswordResetTokens
      .Where(t => t.UserId == userId
                  && t.CreatedAt >= cutoffTime
                  && t.UsedAt == null)
      .OrderByDescending(t => t.CreatedAt)
      .FirstOrDefaultAsync();
  }

  public async Task InvalidateTokensByUserIdAsync(int userId)
  {
    var tokens = await _context.PasswordResetTokens
      .Where(t => t.UserId == userId && t.UsedAt == null)
      .ToListAsync();

    foreach (var token in tokens)
    {
      token.UsedAt = DateTime.UtcNow;
    }
  }

  public PasswordResetToken Create(PasswordResetToken passwordResetToken)
  {
    _context.PasswordResetTokens.Add(passwordResetToken);
    return passwordResetToken;
  }

  public PasswordResetToken Update(PasswordResetToken token)
  {
    _context.PasswordResetTokens.Update(token);
    return token;
  }
}