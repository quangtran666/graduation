using App.Application.Common.Models;

namespace App.Application.Auth.Commands.Register;

public record RegisterResult(
  string Message,
  UserData User
);