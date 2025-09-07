namespace App.Contract.Auth.Requests;

public record ResendEmailVerificationRequest(
  string Email
);