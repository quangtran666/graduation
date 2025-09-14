namespace App.Contract.Auth.Responses;

public record GetCurrentUserResponse(
  string Message,
  UserInfo User
);