using System.Net;
using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) : IProductService
{
    private const string AllProductsCacheKey = "AllProducts";
    
    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        // cache aside design pattern
        // 1. exist cache 2. from db. 3. caching data

        var cachedProducts = await cacheService.GetAsync<List<ProductDto>>(AllProductsCacheKey);
        if (cachedProducts is not null)
            return ServiceResult<List<ProductDto>>.Success(cachedProducts);
        
        
        // throw new CriticalException("Kritik hata oluştu");
        var products = await productRepository.GetAllAsync();
        var productsAsDto = mapper.Map<List<ProductDto>>(products);
        await cacheService.SetAsync(AllProductsCacheKey, productsAsDto, TimeSpan.FromMinutes(1));
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);
        var productsAsDto = mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        var productsAsDto = mapper.Map<List<ProductDto>>(products);
        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return ServiceResult<ProductDto>.Failure($"Product with id: {id} was not found", HttpStatusCode.NotFound)!;
        var productAsDto = mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var existingProductName = await productRepository.AnyAsync(x => x.Name == request.Name); 
        if (existingProductName) return ServiceResult<CreateProductResponse>.Failure("Ürün ismi veritabanında mevcut.");
        var product = mapper.Map<Product>(request);
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"/api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return ServiceResult.Failure($"Product with id: {id} was not found", HttpStatusCode.NotFound);
        var existingProductName = await productRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);
        if (existingProductName) return ServiceResult.Failure("Ürün ismi veritabanında mevcut.");
        product = mapper.Map(request, product);
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product is null) return ServiceResult.Failure($"Product with id: {request.ProductId} was not found");
        product.Stock = request.Quantity;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        productRepository.Delete((await productRepository.GetByIdAsync(id))!);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}