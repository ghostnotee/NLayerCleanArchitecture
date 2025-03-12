using System.Linq.Expressions;

namespace App.Application.Contracts.Persistence;

public interface IGenericRepository<TEntity, in TId> where TEntity : class where TId : struct
{
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    Task<bool> AnyAsync(TId id);
    ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    ValueTask<TEntity?> GetByIdAsync(int id);
    ValueTask AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}