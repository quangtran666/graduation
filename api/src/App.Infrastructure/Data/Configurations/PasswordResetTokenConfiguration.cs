using App.Domain.Entities;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class PasswordResetTokenConfiguration
  : BaseAuditableConfiguration<PasswordResetToken>
{
  const int TOKEN_MAX_LENGTH = 255;

  public override void Configure(EntityTypeBuilder<PasswordResetToken> builder)
  {
    base.Configure(builder);

    builder.ToTable("token_dat_lai_mat_khau");
    builder.Property(x => x.UserId).HasColumnName("ma_nguoi_dung").IsRequired();
    builder
      .Property(x => x.Token)
      .HasColumnName("token")
      .HasMaxLength(TOKEN_MAX_LENGTH)
      .IsRequired();
    builder
      .Property(x => x.ExpiresAt)
      .HasColumnName("het_han_luc")
      .IsRequired();
    builder.Property(x => x.UsedAt).HasColumnName("su_dung_luc");
    builder
      .HasOne(x => x.User)
      .WithMany(x => x.PasswordResetTokens)
      .HasForeignKey(x => x.UserId)
      .OnDelete(DeleteBehavior.Cascade);
    builder.HasQueryFilter(x => x.User.DeletedAt == null);
  }
}