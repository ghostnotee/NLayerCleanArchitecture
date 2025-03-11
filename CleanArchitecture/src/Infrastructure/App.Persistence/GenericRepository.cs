using System.Linq.Expressions;
using App.Application.Contracts.Persistence;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class GenericRepository<TEntity, TId>(AppDbContext context)
    : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : struct
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    protected readonly AppDbContext Context = context;
    public async Task<List<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize) =>
        await _dbSet.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();
    public async ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.AnyAsync(predicate);
    public ValueTask<TEntity?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
    public async ValueTask AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
    public void Update(TEntity entity) => _dbSet.Update(entity);
    public void Delete(TEntity entity) => _dbSet.Remove(entity);
}