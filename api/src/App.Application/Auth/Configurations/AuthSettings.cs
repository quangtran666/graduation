namespace App.Application.Auth.Configurations;

public class AuthSettings
{
  public const string SectionName = "Auth";

  public int EmailVerificationTokenExpirationMinutes { get; set; } = 30;
}