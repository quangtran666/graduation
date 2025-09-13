using App.Application.Auth.Configurations;
using App.Application.Auth.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace App.Infrastructure.Services;

public class AuthCookieService : IAuthCookieService
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthCookieSettings _cookieSettings;

  public AuthCookieService(IHttpContextAccessor httpContextAccessor, IOptions<AuthCookieSettings> cookieSettings)
  {
    _httpContextAccessor = httpContextAccessor;
    _cookieSettings = cookieSettings.Value;
  }

  public void SetAccessTokenCookie(string accessToken)
  {
    var response = _httpContextAccessor.HttpContext?.Response;
    if (response == null) return;

    var cookieOptions = new CookieOptions
    {
      HttpOnly = _cookieSettings.HttpOnly,
      Secure = _cookieSettings.Secure,
      SameSite = Enum.Parse<SameSiteMode>(_cookieSettings.SameSite),
      Path = _cookieSettings.Path,
      Expires = DateTimeOffset.UtcNow.AddMinutes(_cookieSettings.AccessTokenExpiryMinutes)
    };

    if (!string.IsNullOrEmpty(_cookieSettings.Domain))
    {
      cookieOptions.Domain = _cookieSettings.Domain;
    }

    response.Cookies.Append(_cookieSettings.AccessTokenCookieName, accessToken, cookieOptions);
  }

  public void SetRefreshTokenCookie(string refreshToken)
  {
    var response = _httpContextAccessor.HttpContext?.Response;
    if (response == null) return;

    var cookieOptions = new CookieOptions
    {
      HttpOnly = _cookieSettings.HttpOnly,
      Secure = _cookieSettings.Secure,
      SameSite = Enum.Parse<SameSiteMode>(_cookieSettings.SameSite),
      Path = _cookieSettings.Path,
      Expires = DateTimeOffset.UtcNow.AddDays(_cookieSettings.RefreshTokenExpiryDays)
    };

    if (!string.IsNullOrEmpty(_cookieSettings.Domain))
    {
      cookieOptions.Domain = _cookieSettings.Domain;
    }

    response.Cookies.Append(_cookieSettings.RefreshTokenCookieName, refreshToken, cookieOptions);
  }

  public void RemoveAuthCookies()
  {
    var response = _httpContextAccessor.HttpContext?.Response;
    if (response == null) return;

    var cookieOptions = new CookieOptions
    {
      HttpOnly = _cookieSettings.HttpOnly,
      Secure = _cookieSettings.Secure,
      SameSite = Enum.Parse<SameSiteMode>(_cookieSettings.SameSite),
      Path = _cookieSettings.Path,
      Expires = DateTimeOffset.UtcNow.AddDays(-1) // Remove cookie by setting expiration in the past
    };

    if (!string.IsNullOrEmpty(_cookieSettings.Domain))
    {
      cookieOptions.Domain = _cookieSettings.Domain;
    }

    response.Cookies.Append(_cookieSettings.AccessTokenCookieName, "", cookieOptions);
    response.Cookies.Append(_cookieSettings.RefreshTokenCookieName, "", cookieOptions);
  }

  public string? GetAccessTokenFromCookie()
  {
    var request = _httpContextAccessor.HttpContext?.Request;
    return request?.Cookies[_cookieSettings.AccessTokenCookieName];
  }

  public string? GetRefreshTokenFromCookie()
  {
    var request = _httpContextAccessor.HttpContext?.Request;
    return request?.Cookies[_cookieSettings.RefreshTokenCookieName];
  }
}