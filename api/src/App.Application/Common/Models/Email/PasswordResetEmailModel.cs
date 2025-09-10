namespace App.Application.Common.Models.Email;

public record PasswordResetEmailModel(
  string Username,
  string Email,
  string ResetToken,
  DateTime ExpiresAt
);