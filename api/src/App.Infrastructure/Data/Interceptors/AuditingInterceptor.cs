using App.Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Infrastructure.Data.Interceptors;

public class AuditingInterceptor : SaveChangesInterceptor
{
  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    UpdateAuditingProperties(eventData.Context);
    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    UpdateAuditingProperties(eventData.Context);
    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static void UpdateAuditingProperties(DbContext? context)
  {
    if (context is null)
      return;

    foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
    {
      switch (entry.State)
      {
        case EntityState.Added:
          entry.Entity.CreatedAt = DateTime.UtcNow;
          break;
        case EntityState.Modified:
          entry.Entity.UpdatedAt = DateTime.UtcNow;
          break;
        case EntityState.Deleted:
          if (entry.Entity is ISoftDeletable softDeletable)
          {
            entry.State = EntityState.Modified;
            softDeletable.DeletedAt = DateTime.UtcNow;
            entry.Entity.UpdatedAt = DateTime.UtcNow;
          }
          break;
      }
    }
  }
}