using App.Domain.Entities;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : BaseAuditableConfiguration<UserRole>
{
  public override void Configure(EntityTypeBuilder<UserRole> builder)
  {
    base.Configure(builder);

    builder.ToTable("nguoi_dung_vai_tro");
    builder.Property(x => x.UserId).HasColumnName("ma_nguoi_dung").IsRequired();
    builder.Property(x => x.RoleId).HasColumnName("ma_vai_tro").IsRequired();
    builder
      .HasOne(x => x.User)
      .WithMany(x => x.UserRoles)
      .HasForeignKey(x => x.UserId)
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasOne(x => x.Role)
      .WithMany()
      .HasForeignKey(x => x.RoleId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}