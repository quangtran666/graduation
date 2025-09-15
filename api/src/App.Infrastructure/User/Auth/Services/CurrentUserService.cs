using System.Security.Claims;

using App.Application.User.Auth.Constants;
using App.Application.User.Auth.Services;

using ErrorOr;

using Microsoft.AspNetCore.Http;

namespace App.Infrastructure.User.Auth.Services;

public class CurrentUserService : ICurrentUserService
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public CurrentUserService(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public ErrorOr<int> GetCurrentUserId()
  {
    var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
      return Error.Unauthorized(AuthErrors.Token.INVALID_USER_ID, "Invalid user ID in token");

    return userId;
  }

  public ErrorOr<string> GetCurrentUserEmail()
  {
    var emailClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

    if (string.IsNullOrEmpty(emailClaim))
      return Error.Unauthorized(AuthErrors.Token.INVALID_TOKEN, "Email not found in token");

    return emailClaim;
  }

  public ErrorOr<string> GetCurrentUsername()
  {
    var usernameClaim = _httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.USERNAME)?.Value;

    if (string.IsNullOrEmpty(usernameClaim))
      return Error.Unauthorized(AuthErrors.Token.INVALID_TOKEN, "Username not found in token");

    return usernameClaim;
  }

  public ErrorOr<bool> IsEmailVerified()
  {
    var emailVerifiedClaim = _httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.EMAIL_VERIFIED)?.Value;

    if (emailVerifiedClaim == null || !bool.TryParse(emailVerifiedClaim, out var isEmailVerified))
      return Error.Unauthorized(AuthErrors.Token.INVALID_TOKEN, "Email verification status not found in token");

    return isEmailVerified;
  }
}