namespace App.Application.Auth.Constants;

public static class AuthErrors
{
  public static class User
  {
    public const string EMAIL_ALREADY_VERIFIED = "User.EmailAlreadyVerified";
    public const string ALREADY_EXISTS = "User.AlreadyExists";
    public const string NOT_FOUND = "User.NotFound";
    public const string EMAIL_NOT_VERIFIED = "User.EmailNotVerified";
    public const string EMAIL_NOT_VERIFIED_RESENT = "User.EmailNotVerifiedResent";
    public const string ACCOUNT_SUSPENDED = "User.AccountSuspended";
    public const string ACCOUNT_BANNED = "User.AccountBanned";
    public const string INVALID_CREDENTIALS = "User.InvalidCredentials";
  }

  public static class Token
  {
    public const string INVALID_USER_ID = "Token.InvalidUserId";
    public const string INVALID_TOKEN = "Token.InvalidToken";
    public const string EXPIRED_TOKEN = "Token.ExpiredToken";
  }

  public static class EmailVerification
  {
    public const string COOLDOWN_ACTIVE = "EmailVerification.CooldownActive";
    public const string TOKEN_NOT_FOUND = "EmailVerification.TokenNotFound";
    public const string TOKEN_EXPIRED = "EmailVerification.TokenExpired";
    public const string TOKEN_ALREADY_USED = "EmailVerification.TokenAlreadyUsed";
  }

  public static class PasswordReset
  {
    public const string COOLDOWN_ACTIVE = "PasswordReset.CooldownActive";
    public const string TOKEN_NOT_FOUND = "PasswordReset.TokenNotFound";
    public const string TOKEN_EXPIRED = "PasswordReset.TokenExpired";
    public const string TOKEN_ALREADY_USED = "PasswordReset.TokenAlreadyUsed";
  }
}