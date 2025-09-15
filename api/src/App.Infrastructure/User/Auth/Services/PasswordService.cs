using App.Application.User.Auth.Services;

using static BCrypt.Net.BCrypt;

namespace App.Infrastructure.User.Auth.Services;

public class PasswordService : IPasswordService
{
  public string HashPassword(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password);
  }

  public bool VerifyPassword(string password, string hashedPassword)
  {
    return Verify(password, hashedPassword);
  }
}