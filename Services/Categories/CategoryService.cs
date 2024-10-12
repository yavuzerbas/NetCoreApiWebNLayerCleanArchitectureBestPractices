using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Categories
{
    public class CategoryService(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return ServiceResult<CategoryDto>
                    .Fail("Category not found", System.Net.HttpStatusCode.NotFound);
            }

            var categoryDto = mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryDto);
        }
        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int id)
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync(id);

            if (category == null)
            {
                return ServiceResult<CategoryWithProductsDto>
                    .Fail("Category not found", System.Net.HttpStatusCode.NotFound);
            }

            var categoryWithProductsDto = mapper.Map<CategoryWithProductsDto>(category);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryWithProductsDto);


        }
        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var category = await categoryRepository.GetCategoryWithProducts().ToListAsync();

            var categoryWithProductsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryWithProductsDto);
        }
        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(categoriesDto);
        }
        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {

            var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();

            if (anyCategory)
            {
                return ServiceResult<int>.Fail(
                    "Category name already exist in the db.",
                    System.Net.HttpStatusCode.NotFound);
            }

            var newCategory = mapper.Map<Category>(request);
            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.SuccessAsCreated(newCategory.Id, $"api/categories/{newCategory.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request)
        {
            var category = await categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return ServiceResult
                    .Fail("Category not found", System.Net.HttpStatusCode.NotFound);
            }
            var anyCategory = await categoryRepository.Where(x => x.Name == request.Name && x.Id != request.Id).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult.Fail(
                    "Category name already exist in the db.",
                    System.Net.HttpStatusCode.NotFound);
            }
            category = mapper.Map(request, category);
            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult
                    .Fail("Category cannot be found!", System.Net.HttpStatusCode.NotFound);
            }

            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

    }
}
