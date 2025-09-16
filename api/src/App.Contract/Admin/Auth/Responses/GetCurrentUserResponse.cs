namespace App.Contract.Admin.Auth.Responses;

public record GetCurrentUserResponse(
  string Message,
  UserInfo User
);