using App.Domain.Common;

namespace App.Domain.Entities;

public class UserPermission : IAuditable
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int UserId { get; set; }
  public int PermissionId { get; set; }
  public bool IsGranted { get; set; } = true;

  public User User { get; set; } = null!;
  public Permission Permission { get; set; } = null!;
}