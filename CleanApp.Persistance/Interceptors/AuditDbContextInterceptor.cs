using CleanApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanApp.Persistence.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
        {
            {EntityState.Added, AddBehavior },
            {EntityState.Modified, ModifiedBehavior }
        };
        private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
        {
            auditEntity.Created = DateTime.Now.ToUniversalTime();
            context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity)
        {
            context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
            auditEntity.Updated = DateTime.Now.ToUniversalTime();
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {

            foreach (var entityEntry in eventData.Context.ChangeTracker.Entries().ToList())
            {
                if (entityEntry.Entity is not IAuditEntity auditEntity) continue;
                if (Behaviors.TryGetValue(entityEntry.State, out var behavior))
                {
                    behavior(eventData.Context, auditEntity);
                }
                /*if (entityEntry.Entity is EntityState.Added || entityEntry.Entity is EntityState.Modified)
                {
                    Behaviors[entityEntry.State](eventData.Context, auditEntity);
                }*/
                #region without using delegate
                /*switch (entityEntry.State)
                {
                    case EntityState.Added:
                        AddBehavior(eventData.Context, auditEntity);
                        break;

                    case EntityState.Modified:
                        ModifiedBehavior(eventData.Context, auditEntity);
                        break;
                }*/
                #endregion
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
