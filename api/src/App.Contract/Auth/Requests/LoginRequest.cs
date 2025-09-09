namespace App.Contract.Auth.Requests;

public record LoginRequest(
  string UsernameOrEmail,
  string Password,
  bool RememberMe = false
);