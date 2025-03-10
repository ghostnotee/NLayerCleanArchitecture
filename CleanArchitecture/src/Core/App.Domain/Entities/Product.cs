using App.Domain.Entities.Common;

namespace App.Domain.Entities;

public class Product : BaseEntity<int>, IAuditEntity
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}