using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
  const int USERNAME_MAX_LENGTH = 50;
  const int EMAIL_MAX_LENGTH = 255;
  const int PASSWORD_HASH_MAX_LENGTH = 255;
  const int SUSPENSION_REASON_MAX_LENGTH = 500;

  public override void Configure(EntityTypeBuilder<User> builder)
  {
    base.Configure(builder);

    builder.ToTable("nguoi_dung");
    builder
      .Property(x => x.Username)
      .HasColumnName("ten_dang_nhap")
      .HasMaxLength(USERNAME_MAX_LENGTH)
      .IsRequired();
    builder
      .Property(x => x.Email)
      .HasColumnName("email")
      .HasMaxLength(EMAIL_MAX_LENGTH)
      .IsRequired();
    builder
      .Property(x => x.PasswordHash)
      .HasColumnName("mat_khau_hash")
      .HasMaxLength(PASSWORD_HASH_MAX_LENGTH)
      .IsRequired();
    builder
      .Property(x => x.EmailVerified)
      .HasColumnName("email_da_xac_thuc")
      .HasDefaultValue(false);
    builder
      .Property(x => x.Status)
      .HasColumnName("trang_thai")
      .HasConversion<int>()
      .HasDefaultValue(UserStatus.Active)
      .HasSentinel(0);
    builder
      .Property(x => x.TokenVersion)
      .HasColumnName("phien_ban_token")
      .HasDefaultValue(1);
    builder
      .Property(x => x.SuspendedUntil)
      .HasColumnName("khoa_den_luc");
    builder
      .Property(x => x.SuspensionReason)
      .HasColumnName("ly_do_khoa")
      .HasMaxLength(SUSPENSION_REASON_MAX_LENGTH);
    builder
      .Property(x => x.SuspendedBy)
      .HasColumnName("khoa_boi");
  }
}