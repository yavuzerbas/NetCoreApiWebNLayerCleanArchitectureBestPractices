using CleanApp.Application.Contracts.Persistence;

namespace CleanApp.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

}
