namespace App.Application.Auth.Commands.RefreshTokens;

public record RefreshTokenResult(
  string AccessToken,
  string RefreshToken
);