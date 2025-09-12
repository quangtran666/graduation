namespace App.Application.Auth.Configurations;

public class AuthSettings
{
  public const string SectionName = "Auth";

  public int RefreshTokenExpirationDays { get; set; } = 30;
  public int EmailVerificationTokenExpirationMinutes { get; set; } = 30;
  public int PasswordResetTokenExpirationHours { get; set; } = 1;
}