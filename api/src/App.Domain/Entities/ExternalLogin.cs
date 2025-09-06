using App.Domain.Common;

namespace App.Domain.Entities;

public class ExternalLogin : IAuditable
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }

  public int UserId { get; set; }
  public string Provider { get; set; } = string.Empty;
  public string ExternalId { get; set; } = string.Empty;
  public string? Email { get; set; }
  public string? DisplayName { get; set; }
  public string? AccessToken { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime? TokenExpiresAt { get; set; }
  public DateTime? RefreshTokenExpiresAt { get; set; }
  public string? Scope { get; set; }

  public virtual User User { get; set; } = null!;
}