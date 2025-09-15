using App.Application.Common.Models;

namespace App.Application.User.Auth.Commands.Register;

public record RegisterResult(
  string Message,
  UserData User
);