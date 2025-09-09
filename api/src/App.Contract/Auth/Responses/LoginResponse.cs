namespace App.Contract.Auth.Responses;

public record LoginResponse(
  string Message,
  string AccessToken,
  string RefreshToken,
  UserInfo User,
  bool IsRememberMe // Cần implement ở frontend: false = sessionStorage, true = localStorage
);