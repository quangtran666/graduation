using App.Domain.Entities;
using App.Infrastructure.Data.Configurations.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations;

public class RoleConfiguration : BaseAuditableConfiguration<Role>
{
  const int NAME_MAX_LENGTH = 50;
  const int DISPLAY_NAME_MAX_LENGTH = 100;
  const int DESCRIPTION_MAX_LENGTH = 500;

  public override void Configure(EntityTypeBuilder<Role> builder)
  {
    base.Configure(builder);

    builder.ToTable("vai_tro");

    builder
      .Property(x => x.Name)
      .HasColumnName("ten")
      .HasMaxLength(NAME_MAX_LENGTH)
      .IsRequired();
    builder
      .Property(x => x.DisplayName)
      .HasColumnName("ten_hien_thi")
      .HasMaxLength(DISPLAY_NAME_MAX_LENGTH)
      .IsRequired();
    builder
      .Property(x => x.Description)
      .HasColumnName("mo_ta")
      .HasMaxLength(DESCRIPTION_MAX_LENGTH);
    builder
      .Property(x => x.IsDefault)
      .HasColumnName("la_mac_dinh")
      .HasDefaultValue(false);
  }
}