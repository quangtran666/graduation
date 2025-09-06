
using App.Application.Common.Data;
using App.Domain.Entities;

namespace App.Infrastructure.Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
  private readonly AppDbContext _context;

  public RefreshTokenRepository(AppDbContext context)
  {
    _context = context;
  }

  public RefreshToken Create(RefreshToken refreshToken)
  {
    _context.RefreshTokens.Add(refreshToken);
    return refreshToken;
  }
}