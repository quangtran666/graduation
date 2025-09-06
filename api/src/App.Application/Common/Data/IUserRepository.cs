using App.Domain.Entities;

namespace App.Application.Common.Data;

public interface IUserRepository
{
  Task<bool> ExistsAsync(string username, string email);
  User Create(User user);
}