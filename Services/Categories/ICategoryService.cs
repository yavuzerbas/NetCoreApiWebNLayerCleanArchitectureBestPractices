﻿using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;

namespace App.Services.Categories
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
