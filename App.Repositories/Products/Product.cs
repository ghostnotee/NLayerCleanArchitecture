using Repositories.Categories;

namespace Repositories.Products;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
}