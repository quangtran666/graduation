
using App.Application.Common.Data;
using App.Domain.Entities;

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
      .Where(x => x.Email == email)
      .Select(u => new UserDomain
      {
        Id = u.Id,
        Email = u.Email,
        Username = u.Username,
        PasswordHash = u.PasswordHash,
        UserRoles = u.UserRoles.Select(ur => new UserRole
        {
          UserId = ur.UserId,
          RoleId = ur.RoleId,
          Role = new Role
          {
            Id = ur.Role.Id,
            Name = ur.Role.Name
          }
        }).ToList()
      })
      .FirstOrDefaultAsync();
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