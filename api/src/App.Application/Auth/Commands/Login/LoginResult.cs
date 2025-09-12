using App.Application.Common.Models;

namespace App.Application.Auth.Commands.Login;

public record LoginResult(
  string Message,
  UserData User
);