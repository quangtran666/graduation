namespace App.Application.Auth.Constants;

public static class AuthConstants
{
  public static class EmailVerification
  {
    public const int COOLDOWN_SECONDS = 60;
  }

  public static class ResponseDetails
  {
    public const string EMAIL_NOT_VERIFIED_RESENT = "email_not_verified_resent";
  }
}