namespace App.Application.Auth.Commands.ResendEmailVerification;

public record ResendEmailVerificationResult(
  string Message,
  int CooldownSeconds
);