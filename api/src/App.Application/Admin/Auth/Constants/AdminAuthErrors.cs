namespace App.Application.Admin.Auth.Constants;

public static class AdminAuthErrors
{
  public static class User
  {
    public const string NOT_FOUND = "User.NotFound";
    public const string INVALID_CREDENTIALS = "User.InvalidCredentials";
    public const string EMAIL_NOT_VERIFIED = "User.EmailNotVerified";
    public const string ACCOUNT_BANNED = "User.AccountBanned";
    public const string ACCOUNT_SUSPENDED = "User.AccountSuspended";
    public const string NOT_ADMIN = "User.NotAdmin";
  }
}