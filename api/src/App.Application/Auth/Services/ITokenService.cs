using ErrorOr;

using UserDomain = App.Domain.Entities.User;

namespace App.Application.Auth.Services;

public interface ITokenService
{
  string GenerateAccessToken(UserDomain user);
  string GenerateRefreshToken();
  ErrorOr<bool> ValidateAccessToken(string token);
  ErrorOr<int?> GetUserIdFromAccessToken(string token);
  ErrorOr<int?> GetTokenVersionFromAccessToken(string token);
}