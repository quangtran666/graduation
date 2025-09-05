using App.Domain.Entities;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class ExternalLoginConfiguration
  : BaseAuditableConfiguration<ExternalLogin>
{
  const int PROVIDER_MAX_LENGTH = 50;
  const int EXTERNAL_ID_MAX_LENGTH = 255;
  const int EMAIL_MAX_LENGTH = 255;
  const int DISPLAY_NAME_MAX_LENGTH = 255;
  const int ACCESS_TOKEN_MAX_LENGTH = 1000;
  const int REFRESH_TOKEN_MAX_LENGTH = 1000;
  const int SCOPE_MAX_LENGTH = 200;

  public override void Configure(EntityTypeBuilder<ExternalLogin> builder)
  {
    base.Configure(builder);

    builder.ToTable("dang_nhap_ngoai");
    builder.Property(x => x.UserId).HasColumnName("ma_nguoi_dung").IsRequired();
    builder
      .Property(x => x.Provider)
      .HasColumnName("nha_cung_cap")
      .HasMaxLength(PROVIDER_MAX_LENGTH)
      .IsRequired();
    builder
      .Property(x => x.ExternalId)
      .HasColumnName("ma_ngoai")
      .HasMaxLength(EXTERNAL_ID_MAX_LENGTH)
      .IsRequired();
    builder
      .Property(x => x.Email)
      .HasColumnName("email")
      .HasMaxLength(EMAIL_MAX_LENGTH);
    builder
      .Property(x => x.DisplayName)
      .HasColumnName("ten_hien_thi")
      .HasMaxLength(DISPLAY_NAME_MAX_LENGTH);
    builder
      .Property(x => x.AccessToken)
      .HasColumnName("token_truy_cap")
      .HasMaxLength(ACCESS_TOKEN_MAX_LENGTH);
    builder
      .Property(x => x.RefreshToken)
      .HasColumnName("token_lam_moi")
      .HasMaxLength(REFRESH_TOKEN_MAX_LENGTH);
    builder
      .Property(x => x.TokenExpiresAt)
      .HasColumnName("het_han_luc_token_truy_cap");
    builder
      .Property(x => x.RefreshTokenExpiresAt)
      .HasColumnName("het_han_luc_token_lam_moi");
    builder
      .Property(x => x.Scope)
      .HasColumnName("pham_vi")
      .HasMaxLength(SCOPE_MAX_LENGTH);
    builder
      .HasOne(x => x.User)
      .WithMany(x => x.ExternalLogins)
      .HasForeignKey(x => x.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}