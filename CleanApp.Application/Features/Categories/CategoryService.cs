using AutoMapper;
using CleanApp.Application.Contracts.Persistance;
using CleanApp.Application.Features.Categories.Create;
using CleanApp.Application.Features.Categories.Dto;
using CleanApp.Application.Features.Categories.Update;
using CleanApp.Domain.Entities;

namespace CleanApp.Application.Features.Categories
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
            //var category = await categoryRepository.GetCategoryWithProducts().ToListAsync();
            var category = await categoryRepository.GetCategoryWithProductsAsync();

            var categoryWithProductsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryWithProductsDto);
        }
        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            //var categories = await categoryRepository.GetAll().ToListAsync();
            var categories = await categoryRepository.GetAllAsync();
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(categoriesDto);
        }
        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {

            //var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
            var anyCategory = await categoryRepository.AnyAsync(x => x.Name == request.Name);

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

            //var anyCategory = await categoryRepository.Where(x => x.Name == request.Name && x.Id != request.Id).AnyAsync();
            var anyCategory = await categoryRepository.AnyAsync(x => x.Name == request.Name && x.Id != request.Id);
            if (anyCategory)
            {
                return ServiceResult.Fail(
                    "Category name already exist in the db.",
                    System.Net.HttpStatusCode.NotFound);
            }
            var category = mapper.Map<Category>(request);

            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            categoryRepository.Delete(category); //checked null case in NotFoundFilter
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

    }
}
