namespace App.Contract.Auth.Responses;

public record RegisterResponse(
  string Message,
  UserInfo User
);