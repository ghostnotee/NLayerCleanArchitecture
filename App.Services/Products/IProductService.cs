namespace Services.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetAllList();
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(UpdateProductRequest request);
    Task<ServiceResult> DeleteAsync(int id);
}