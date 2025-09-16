using App.Application.Common.Models;

namespace App.Application.Admin.Auth.Commands.Login;

public record LoginResult(
  string Message,
  UserData User
);