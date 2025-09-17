
using App.Application.Common.Data;

using Microsoft.EntityFrameworkCore;

using UserDomain = App.Domain.Entities.User;

namespace App.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<UserDomain?> GetByEmailAsync(string email)
  {
    return await _context.Users
      .FirstOrDefaultAsync(x => x.Email == email);
  }

  public async Task<UserDomain?> GetByIdAsync(int id)
  {
    return await _context.Users
      .FirstOrDefaultAsync(x => x.Id == id);
  }

  public async Task<UserDomain?> GetByEmailOrUsernameAsync(string emailOrUsername)
  {
    return await _context.Users
      .FirstOrDefaultAsync(x => x.Email == emailOrUsername || x.Username == emailOrUsername);
  }

  public async Task<UserDomain?> GetByEmailWithRolesAsync(string email)
  {
    return await _context.Users
      .AsNoTracking()
      .AsSplitQuery()
      .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
      .FirstOrDefaultAsync(u => u.Email == email);
  }

  public UserDomain Create(UserDomain user)
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