namespace App.Contract.Auth.Requests;

public record RegisterRequest(
  string Username,
  string Email,
  string Password,
  string ConfirmPassword
);