using App.Domain.Common;

namespace App.Domain.Entities;

public class Permission : IAuditable
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public string Name { get; set; } = string.Empty;
  public string DisplayName { get; set; } = string.Empty;
  public string? Description { get; set; }
  public string Category { get; set; } = string.Empty;
}