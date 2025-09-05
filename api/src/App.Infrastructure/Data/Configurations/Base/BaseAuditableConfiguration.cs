using App.Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations.Base;

public class BaseAuditableConfiguration<T> : IEntityTypeConfiguration<T>
  where T : class, IAuditable
{
  public virtual void Configure(EntityTypeBuilder<T> builder)
  {
    builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
    builder.Property(x => x.CreatedAt).HasColumnName("ngay_tao").IsRequired();
    builder.Property(x => x.UpdatedAt).HasColumnName("ngay_cap_nhat");
  }
}