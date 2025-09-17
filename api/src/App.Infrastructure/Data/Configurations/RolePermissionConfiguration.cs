using App.Domain.Entities;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class RolePermissionConfiguration
  : BaseAuditableConfiguration<RolePermission>
{
  public override void Configure(EntityTypeBuilder<RolePermission> builder)
  {
    base.Configure(builder);

    builder.ToTable("vai_tro_quyen");
    builder.Property(x => x.RoleId).HasColumnName("ma_vai_tro").IsRequired();
    builder
      .Property(x => x.PermissionId)
      .HasColumnName("ma_quyen")
      .IsRequired();
    builder
      .HasOne(x => x.Role)
      .WithMany(x => x.RolePermissions)
      .HasForeignKey(x => x.RoleId)
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasOne(x => x.Permission)
      .WithMany(x => x.RolePermissions)
      .HasForeignKey(x => x.PermissionId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}