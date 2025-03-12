using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
{
    public Task<List<Category>> GetCategoriesWithProductsAsync()
    {
        return Context.Categories.Include(x => x.Products).AsQueryable().ToListAsync();
    }

    public Task<Category?> GetCategoryWithProductsAsync(int categoryId)
    {
        return Context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
    }

    public IQueryable<Category> GetCategoriesWithProducts()
    {
        return Context.Categories.Include(x => x.Products).AsQueryable();
    }
}