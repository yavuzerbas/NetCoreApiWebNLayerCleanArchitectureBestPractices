using CleanApp.Domain.Entities;

namespace CleanApp.Application.Contracts.Persistance
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {
        Task<Category?> GetCategoryWithProductsAsync(int id);
        Task<List<Category>> GetCategoryWithProductsAsync();

    }
}
