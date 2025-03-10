namespace Repositories.Categories;

public interface ICategoryRepository : IGenericRepository<Category,int>
{
    IQueryable<Category> GetCategoriesWithProducts();
    Task<Category?> GetCategoryWithProductsAsync(int categoryId);
}