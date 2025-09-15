using UserDomain = App.Domain.Entities.User;

namespace App.Application.Common.Data;

public interface IUserRepository
{
  Task<UserDomain?> GetByEmailAsync(string email);
  Task<UserDomain?> GetByIdAsync(int id);
  Task<UserDomain?> GetByEmailOrUsernameAsync(string emailOrUsername);
  Task<bool> ExistsAsync(string username, string email);
  UserDomain Create(UserDomain user);
}