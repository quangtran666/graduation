namespace App.Contract.User.Auth.Responses;

public record GetCurrentUserResponse(
  string Message,
  UserInfo User
);