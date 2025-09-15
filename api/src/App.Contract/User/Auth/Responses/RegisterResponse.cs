namespace App.Contract.User.Auth.Responses;

public record RegisterResponse(
  string Message,
  UserInfo User
);