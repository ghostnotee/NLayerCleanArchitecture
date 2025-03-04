using Repositories.Products;

namespace Services.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ServiceResult<List<Product>>> GetTopPriceProductsAsync(int count)
    {
        return new ServiceResult<List<Product>>
        {
            Data = await productRepository.GetTopPriceProductsAsync(count)
        };
    }

    public async Task<ServiceResult<Product>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null) ServiceResult<Product>.Failure($"Product with id: {id} was not found");

        return ServiceResult<Product>.Success(product!);
    }
}