using App.Application.Common.Models;

namespace App.Application.User.Auth.Commands.Login;

public record LoginResult(
  string Message,
  UserData User
);