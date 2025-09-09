using App.Domain.Common;
using App.Domain.Enums;

namespace App.Domain.Entities;

public class User : IEntity
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; }
  public DateTime? DeletedAt { get; set; }
  public int? DeletedBy { get; set; }

  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public bool EmailVerified { get; set; }

  public UserStatus Status { get; set; } = UserStatus.Active;
  public int TokenVersion { get; set; } = 1;
  public DateTime? SuspendedUntil { get; set; }
  public string? SuspensionReason { get; set; }
  public int? SuspendedBy { get; set; }

  public virtual ICollection<EmailVerificationToken> EmailVerificationTokens { get; set; } =
  [];
  public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = [];
  public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = [];
  public virtual ICollection<ExternalLogin> ExternalLogins { get; set; } = [];

  public virtual ICollection<UserRole> UserRoles { get; set; } = [];
  public virtual ICollection<UserPermission> UserPermissions { get; set; } = [];
}