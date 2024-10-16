using CleanApp.Application.Features.Products.Dto;

namespace CleanApp.Application.Features.Categories.Dto
{
    public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);
}
