namespace App.Contract.User.Auth.Requests;

public record LoginRequest(
  string UsernameOrEmail,
  string Password
);