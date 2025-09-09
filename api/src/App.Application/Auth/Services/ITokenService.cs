using App.Domain.Entities;

using ErrorOr;

namespace App.Application.Auth.Services;

public interface ITokenService
{
  string GenerateAccessToken(User user);
  string GenerateRefreshToken();
  ErrorOr<bool> ValidateAccessToken(string token);
  ErrorOr<int?> GetUserIdFromAccessToken(string token);
  ErrorOr<int?> GetTokenVersionFromAccessToken(string token);
}