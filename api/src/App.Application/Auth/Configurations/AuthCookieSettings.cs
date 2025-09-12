namespace App.Application.Auth.Configurations;

public class AuthCookieSettings
{
  public const string Section = "AuthCookieSettings";

  public string AccessTokenCookieName { get; set; } = "auth_access_token";
  public string RefreshTokenCookieName { get; set; } = "auth_refresh_token";
  public string Domain { get; set; } = "";
  public string Path { get; set; } = "/";
  public bool HttpOnly { get; set; } = true;
  public bool Secure { get; set; } = true;
  public string SameSite { get; set; } = "Strict";

  public int AccessTokenExpiryMinutes { get; set; }
  public int RefreshTokenExpiryDays { get; set; }
}