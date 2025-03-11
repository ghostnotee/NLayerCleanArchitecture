using App.Domain.Entities;

namespace App.Application.Contracts.Persistence;

public interface ICategoryRepository : IGenericRepository<Category, int>
{
    Task<List<Category>> GetCategoriesWithProductsAsync();
    Task<Category?> GetCategoryWithProductsAsync(int categoryId);
}