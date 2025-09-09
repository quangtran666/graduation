using App.Domain.Entities;

namespace App.Application.Common.Data;

public interface IRefreshTokenRepository
{
  Task<RefreshToken?> GetByTokenAsync(string token);
  RefreshToken Create(RefreshToken refreshToken);
}