using Services.Categories.Create;
using Services.Categories.Dto;
using Services.Categories.Update;

namespace Services.Categories;

public interface ICategoryService
{
    Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
    Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoriesWithProductsAsync();
    Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int id);
    Task<ServiceResult<List<CategoryDto>>> GetPagedAsync(int pageNumber, int pageSize);
    Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id);
    Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
    Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);

    Task<ServiceResult> DeleteAsync(int id);
}