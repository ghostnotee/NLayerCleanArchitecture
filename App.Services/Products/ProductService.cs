using System.Net;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Products;

namespace Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productsAsDto = products.Select(x => new ProductDto(x.Id, x.Name, x.Price, x.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var productsAsDto = products.Select(x => new ProductDto(x.Id, x.Name, x.Price, x.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        var productsAsDto = products.Select(x => new ProductDto(x.Id, x.Name, x.Price, x.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) ServiceResult<ProductDto>.Failure($"Product with id: {id} was not found", HttpStatusCode.NotFound);
        var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);
        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"/api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return ServiceResult.Failure($"Product with id: {id} was not found");
        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return ServiceResult.Failure($"Product with id: {id} was not found", HttpStatusCode.NotFound);
        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}