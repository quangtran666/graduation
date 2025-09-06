using App.Domain.Entities;

namespace App.Application.Common.Data;

public interface IRefreshTokenRepository
{
  RefreshToken Create(RefreshToken refreshToken);
}