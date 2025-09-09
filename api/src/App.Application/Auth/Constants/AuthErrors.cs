namespace App.Application.Auth.Constants;

public static class AuthErrors
{
  public static class User
  {
    public const string EMAIL_ALREADY_VERIFIED = "User.EmailAlreadyVerified";
    public const string ALREADY_EXISTS = "User.AlreadyExists";
    public const string NOT_FOUND = "User.NotFound";
  }

  public static class Token
  {
    public const string INVALID_USER_ID = "Token.InvalidUserId";
  }

  public static class EmailVerification
  {
    public const string COOLDOWN_ACTIVE = "EmailVerification.CooldownActive";
    public const string TOKEN_NOT_FOUND = "EmailVerification.TokenNotFound";
    public const string TOKEN_EXPIRED = "EmailVerification.TokenExpired";
    public const string TOKEN_ALREADY_USED = "EmailVerification.TokenAlreadyUsed";
  }
}