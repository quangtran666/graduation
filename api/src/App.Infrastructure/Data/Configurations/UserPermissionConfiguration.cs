using App.Domain.Entities;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class UserPermissionConfiguration
  : BaseAuditableConfiguration<UserPermission>
{
  public override void Configure(EntityTypeBuilder<UserPermission> builder)
  {
    base.Configure(builder);

    builder.ToTable("nguoi_dung_quyen");

    builder.Property(x => x.UserId).HasColumnName("ma_nguoi_dung").IsRequired();
    builder
      .Property(x => x.PermissionId)
      .HasColumnName("ma_quyen")
      .IsRequired();
    builder
      .Property(x => x.IsGranted)
      .HasColumnName("duoc_cap_phep")
      .HasDefaultValue(true);
    builder
      .HasOne(x => x.User)
      .WithMany(x => x.UserPermissions)
      .HasForeignKey(x => x.UserId)
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasOne(x => x.Permission)
      .WithMany()
      .HasForeignKey(x => x.PermissionId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}