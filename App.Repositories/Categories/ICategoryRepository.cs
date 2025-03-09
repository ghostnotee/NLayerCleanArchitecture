namespace Repositories.Categories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    IQueryable<Category> GetCategoriesWithProducts();
    Task<Category?> GetCategoryWithProductsAsync(int categoryId);
}