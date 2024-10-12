using App.Services.Products;

namespace App.Repositories.Categories
{
    public record CategoryWIthProductsDto(int Id, string Name, List<ProductDto> Products);
}
