namespace App.Application.Common.Models.Email;

public record PasswordResetCompletedEmailModel(
  string Username,
  string Email
);