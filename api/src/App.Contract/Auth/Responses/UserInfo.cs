namespace App.Contract.Auth.Responses;

public record UserInfo(
  int Id,
  string Username,
  string Email,
  bool EmailVerified
);