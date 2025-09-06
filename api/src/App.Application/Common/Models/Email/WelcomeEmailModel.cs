namespace App.Application.Common.Models.Email;

public record WelcomeEmailModel(
  string Username,
  string Email,
  string VerificationToken,
  DateTime ExpiresAt
);