using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApi.Domain.src.Entities;

namespace WebApi.WebApi.src.Database
{
    public class TimeStampInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var entities = eventData.Context!.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(e => e.Entity)
            .OfType<BaseEntity>();
            var timestamp = DateTime.Now;
            foreach (var entity in entities)
            {
                if (eventData.Context.Entry(entity).State == EntityState.Added)
                {
                    entity.CreatedAt = timestamp;
                    entity.UpdatedAt = timestamp;
                }

                entity.UpdatedAt = timestamp;
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}