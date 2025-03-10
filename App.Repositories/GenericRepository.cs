using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class GenericRepository<TEntity, TId>(AppDbContext context)
    : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : struct
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    protected readonly AppDbContext Context = context;

    public IQueryable<TEntity> GetAll() => _dbSet.AsQueryable().AsNoTracking();
    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();
    public async ValueTask<bool> AnyAsync(TId id) => await _dbSet.AnyAsync(x => x.Id.Equals(id));
    public ValueTask<TEntity?> GetByIdAsync(int id) => _dbSet.FindAsync(id);
    public async ValueTask AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
    public void Update(TEntity entity) => _dbSet.Update(entity);
    public void Delete(TEntity entity) => _dbSet.Remove(entity);
}