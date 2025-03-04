using Repositories.Products;

namespace Services.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public Task<List<Product>> GetTopPriceProductsAsync(int count)
    {
        return productRepository.GetTopPriceProductsAsync(count);
    }
}