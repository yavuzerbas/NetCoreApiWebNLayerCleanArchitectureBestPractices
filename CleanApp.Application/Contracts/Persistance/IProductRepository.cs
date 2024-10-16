using CleanApp.Domain.Entities;

namespace CleanApp.Application.Contracts.Persistance
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        public Task<List<Product>> GetTopPriceProductsAsync(int count);
    }
}
