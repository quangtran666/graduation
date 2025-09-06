
using App.Domain.Common;

namespace App.Domain.Entities;

public class EmailVerificationToken : IAuditable
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int UserId { get; set; }
  public string Token { get; set; } = string.Empty;
  public DateTime ExpiresAt { get; set; }
  public DateTime? UsedAt { get; set; }

  public virtual User User { get; set; } = null!;
}