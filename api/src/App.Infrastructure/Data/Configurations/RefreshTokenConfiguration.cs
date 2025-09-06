using App.Domain.Entities;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration
  : BaseAuditableConfiguration<RefreshToken>
{
  const int TOKEN_MAX_LENGTH = 255;

  public override void Configure(EntityTypeBuilder<RefreshToken> builder)
  {
    base.Configure(builder);

    builder.ToTable("token_lam_moi");
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
    builder
      .Property(x => x.IsRevoked)
      .HasColumnName("da_thu_hoi")
      .HasDefaultValue(false);
    builder
      .HasOne(x => x.User)
      .WithMany(x => x.RefreshTokens)
      .HasForeignKey(x => x.UserId)
      .OnDelete(DeleteBehavior.Cascade);
    builder.HasQueryFilter(x => x.User.DeletedAt == null);
  }
}