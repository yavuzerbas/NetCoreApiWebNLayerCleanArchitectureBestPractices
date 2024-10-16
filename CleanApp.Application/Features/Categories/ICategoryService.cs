using CleanApp.Application.Features.Categories.Create;
using CleanApp.Application.Features.Categories.Dto;
using CleanApp.Application.Features.Categories.Update;

namespace CleanApp.Application.Features.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int id);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request);
    }
}
