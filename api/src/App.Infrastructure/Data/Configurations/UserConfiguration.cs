using App.Domain.Entities;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
  const int USERNAME_MAX_LENGTH = 50;
  const int EMAIL_MAX_LENGTH = 255;
  const int PASSWORD_HASH_MAX_LENGTH = 255;

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
  }
}