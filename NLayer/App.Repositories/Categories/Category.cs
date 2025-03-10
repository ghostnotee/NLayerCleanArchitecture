using Repositories.Products;

namespace Repositories.Categories;

public class Category : BaseEntity<int>, IAuditEntity
{
    public required string Name { get; set; }
    public List<Product>? Products { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}