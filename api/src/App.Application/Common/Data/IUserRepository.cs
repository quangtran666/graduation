using App.Domain.Entities;

namespace App.Application.Common.Data;

public interface IUserRepository
{
  Task<User?> GetByEmailAsync(string email);
  Task<User?> GetByIdAsync(int id);
  Task<bool> ExistsAsync(string username, string email);
  User Create(User user);
}