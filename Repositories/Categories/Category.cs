using App.Repositories.Products;

namespace App.Repositories.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public List<Product>? Products { get; set; }
    }
}
