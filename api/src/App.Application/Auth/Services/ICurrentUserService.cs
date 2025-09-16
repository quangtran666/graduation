using ErrorOr;

namespace App.Application.Auth.Services;

public interface ICurrentUserService
{
  ErrorOr<int> GetCurrentUserId();
  ErrorOr<string> GetCurrentUserEmail();
  ErrorOr<string> GetCurrentUsername();
  ErrorOr<bool> IsEmailVerified();
}