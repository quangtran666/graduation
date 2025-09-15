using App.Application.Common.Models;

namespace App.Application.User.Auth.Commands.VerifyEmail;

public record VerifyEmailResult(
  string Message,
  UserData User
);