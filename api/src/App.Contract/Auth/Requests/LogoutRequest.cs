namespace App.Contract.Auth.Requests;

public record LogoutRequest(
  string RefreshToken
);