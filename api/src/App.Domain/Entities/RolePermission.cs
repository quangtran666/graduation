using App.Domain.Common;

namespace App.Domain.Entities;

public class RolePermission : IAuditable
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int RoleId { get; set; }
  public int PermissionId { get; set; }

  public Role Role { get; set; } = null!;
  public Permission Permission { get; set; } = null!;
}