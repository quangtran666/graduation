using App.Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Configurations.Base;

public class BaseEntityConfiguration<T> : BaseAuditableConfiguration<T>
  where T : class, IEntity
{
  public override void Configure(EntityTypeBuilder<T> builder)
  {
    base.Configure(builder);

    builder.Property(x => x.DeletedAt).HasColumnName("ngay_xoa");
    builder.Property(x => x.DeletedBy).HasColumnName("nguoi_xoa");
  }
}