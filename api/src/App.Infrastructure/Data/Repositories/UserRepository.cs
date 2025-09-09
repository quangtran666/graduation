
using App.Application.Common.Data;
using App.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    return await _context.Users
      .FirstOrDefaultAsync(x => x.Email == email);
  }

  public async Task<User?> GetByIdAsync(int id)
  {
    return await _context.Users
      .FirstOrDefaultAsync(x => x.Id == id);
  }

  public async Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername)
  {
    return await _context.Users
      .FirstOrDefaultAsync(x => x.Email == emailOrUsername || x.Username == emailOrUsername);
  }

  public User Create(User user)
  {
    _context.Users.Add(user);
    return user;
  }

  public async Task<bool> ExistsAsync(string username, string email)
  {
    return await _context.Users
      .AnyAsync(x => x.Username == username || x.Email == email);
  }

}