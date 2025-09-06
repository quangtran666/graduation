
using App.Domain.Common;

namespace App.Domain.Entities;

public class UserRole : IAuditable
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int UserId { get; set; }
  public int RoleId { get; set; }

  public virtual User User { get; set; } = null!;
  public virtual Role Role { get; set; } = null!;
}