namespace App.Contract.Admin.Auth.Responses;

public record LoginResponse(
  string Message,
  UserInfo User
);

public record UserInfo(
  int Id,
  string Username,
  string Email,
  bool EmailVerified
);