namespace App.Contract.User.Auth.Requests;

public record RegisterRequest(
  string Username,
  string Email,
  string Password,
  string ConfirmPassword
);