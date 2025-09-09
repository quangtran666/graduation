namespace App.Contract.Auth.Responses;

public record RefreshTokenResponse(
  string AccessToken,
  string RefreshToken
);