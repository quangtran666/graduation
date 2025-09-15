namespace App.Contract.User.Auth.Requests;

public record ResendEmailVerificationRequest(
  string Email
);