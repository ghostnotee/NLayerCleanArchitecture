using System.Net;
using Repositories;
using Repositories.Products;

namespace Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        var productsAsDto = products.Select(x => new ProductDto(x.Id, x.Name, x.Price, x.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null) ServiceResult<ProductDto>.Failure($"Product with id: {id} was not found");

        var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

        return ServiceResult<ProductDto>.Success(productAsDto!);
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id), HttpStatusCode.Created);
    }

    public async Task<ServiceResult> UpdateProductAsync(UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.Id);

        if (product is null) return ServiceResult.Failure($"Product with id: {request.Id} was not found");

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }
}