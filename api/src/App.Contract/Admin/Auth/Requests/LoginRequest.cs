namespace App.Contract.Admin.Auth.Requests;

public record LoginRequest(
  string Email,
  string Password
);