using App.Domain.Common;

namespace App.Domain.Entities;

public class RolePermission : IAuditable
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int RoleId { get; set; }
  public int PermissionId { get; set; }

  public virtual Role Role { get; set; } = null!;
  public virtual Permission Permission { get; set; } = null!;
}