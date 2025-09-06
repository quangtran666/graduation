namespace App.Contract.Auth.Responses;

public record AuthResponse(
  string AccessToken,
  string RefreshToken,
  DateTime ExpiresAt,
  UserInfo User
);