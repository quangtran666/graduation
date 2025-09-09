using App.Application.Common.Models;

namespace App.Application.Auth.Commands.Login;

public record LoginResult(
  string Message,
  string AccessToken,
  string RefreshToken,
  UserData User,
  bool IsRememberMe // Cần implement ở frontend: false = sessionStorage, true = localStorage
);