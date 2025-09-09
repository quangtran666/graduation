
using App.Application.Common.Data;
using App.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
  private readonly AppDbContext _context;

  public RefreshTokenRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<RefreshToken?> GetByTokenAsync(string token)
  {
    return await _context.RefreshTokens
      .Include(x => x.User)
      .FirstOrDefaultAsync(x => x.Token == token);
  }

  public RefreshToken Create(RefreshToken refreshToken)
  {
    _context.RefreshTokens.Add(refreshToken);
    return refreshToken;
  }
}