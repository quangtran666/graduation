using App.Domain.Common;

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

  public ICollection<EmailVerificationToken> EmailVerificationTokens { get; set; } =
  [];
  public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
  public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = [];
  public ICollection<ExternalLogin> ExternalLogins { get; set; } = [];

  public ICollection<UserRole> UserRoles { get; set; } = [];
  public ICollection<UserPermission> UserPermissions { get; set; } = [];
}