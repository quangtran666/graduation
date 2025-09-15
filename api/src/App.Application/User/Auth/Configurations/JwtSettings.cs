namespace App.Application.User.Auth.Configurations;

public class JwtSettings
{
  public const string SectionName = "Jwt";

  public string SecretKey { get; set; } = string.Empty;
  public string Issuer { get; set; } = string.Empty;
  public string Audience { get; set; } = string.Empty;
  public int AccessTokenExpirationMinutes { get; set; } = 30;
}