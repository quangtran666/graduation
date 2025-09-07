namespace App.Contract.Auth.Responses;

public record ResendEmailVerificationResponse(
  string Message,
  int CooldownSeconds
);