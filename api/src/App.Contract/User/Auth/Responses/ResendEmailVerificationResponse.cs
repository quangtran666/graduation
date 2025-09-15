namespace App.Contract.User.Auth.Responses;

public record ResendEmailVerificationResponse(
  string Message,
  int CooldownSeconds
);