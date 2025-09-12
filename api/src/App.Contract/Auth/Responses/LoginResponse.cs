namespace App.Contract.Auth.Responses;

public record LoginResponse(
  string Message,
  UserInfo User
);