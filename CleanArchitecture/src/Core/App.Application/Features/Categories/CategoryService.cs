using System.Net;
using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoriesWithProductsAsync()
    {
        var categories = await categoryRepository.GetCategoriesWithProductsAsync();
        var categoriesAsDto = mapper.Map<List<CategoryWithProductsDto>>(categories);
        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int id)
    {
        var categories = await categoryRepository.GetCategoryWithProductsAsync(id);
        if (categories == null)
            return ServiceResult<CategoryWithProductsDto>.Failure($"Category with id: {id} was not found", HttpStatusCode.NotFound)!;
        var categoriesAsDto = mapper.Map<CategoryWithProductsDto>(categories);
        return ServiceResult<CategoryWithProductsDto>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<List<CategoryDto>>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var categories = await categoryRepository.GetAllPagedAsync(pageNumber, pageSize);
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null)
            return ServiceResult<CategoryDto>.Failure($"Category with id: {id} was not found", HttpStatusCode.NotFound)!;
        var categoryAsDto = mapper.Map<CategoryDto>(category);
        return ServiceResult<CategoryDto>.Success(categoryAsDto)!;
    }

    public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
    {
        var existingCategoryName = await categoryRepository.AnyAsync(x => x.Name == request.Name);
        if (existingCategoryName) return ServiceResult<int>.Failure("Category name already exists.");
        var category = mapper.Map<Category>(request);
        await categoryRepository.AddAsync(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<int>.SuccessAsCreated(category.Id, $"/api/categories/{category.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null) return ServiceResult.Failure("Category not found.");
        var existingCategoryName = await categoryRepository.AnyAsync(x => x.Name == request.Name && x.Id != id);
        if (existingCategoryName) return ServiceResult.Failure("Category name already exists.");
        category = mapper.Map(request, category);
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null) return ServiceResult.Failure("Category not found.");
        categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}