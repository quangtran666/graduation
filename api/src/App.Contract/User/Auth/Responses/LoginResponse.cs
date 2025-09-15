namespace App.Contract.User.Auth.Responses;

public record LoginResponse(
  string Message,
  UserInfo User
);