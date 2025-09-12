using Microsoft.Extensions.Options;

namespace App.Application.Auth.Configurations;

public class PostConfigureAuthCookieSettings : IPostConfigureOptions<AuthCookieSettings>
{
  private readonly JwtSettings _jwtSettings;
  private readonly AuthSettings _authSettings;

  public PostConfigureAuthCookieSettings(IOptions<JwtSettings> jwtSettings, IOptions<AuthSettings> authSettings)
  {
    _jwtSettings = jwtSettings.Value;
    _authSettings = authSettings.Value;
  }

  public void PostConfigure(string? name, AuthCookieSettings options)
  {
    if (_jwtSettings.AccessTokenExpirationMinutes > 0)
    {
      options.AccessTokenExpiryMinutes = _jwtSettings.AccessTokenExpirationMinutes;
    }

    if (_authSettings.RefreshTokenExpirationDays > 0)
    {
      options.RefreshTokenExpiryDays = _authSettings.RefreshTokenExpirationDays;
    }
  }
}