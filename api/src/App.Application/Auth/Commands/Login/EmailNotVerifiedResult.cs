namespace App.Application.Auth.Commands.Login;

public record EmailNotVerifiedResult(
  string Message,
  string Email,
  int CooldownSeconds
);