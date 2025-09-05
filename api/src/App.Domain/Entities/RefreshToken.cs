
using App.Domain.Common;

namespace App.Domain.Entities;

public class RefreshToken : IAuditable
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int UserId { get; set; }
  public string Token { get; set; } = string.Empty;
  public DateTime ExpiresAt { get; set; }
  public bool IsRevoked { get; set; }

  public User User { get; set; } = null!;
}