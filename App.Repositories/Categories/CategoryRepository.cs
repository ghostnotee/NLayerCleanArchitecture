using Microsoft.EntityFrameworkCore;

namespace Repositories.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
{
    public IQueryable<Category> GetCategoriesWithProducts()
    {
        return Context.Categories.Include(x => x.Products).AsQueryable();
    }

    public Task<Category?> GetCategoryWithProductsAsync(int categoryId)
    {
        return Context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
    }
}