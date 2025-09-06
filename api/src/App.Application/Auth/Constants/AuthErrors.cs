namespace App.Application.Auth.Constants;

public static class AuthErrors
{
  public static class User
  {
    public const string ALREADY_EXISTS = "User.AlreadyExists";
  }

  public static class Token
  {
    public const string INVALID_USER_ID = "Token.InvalidUserId";
  }
}