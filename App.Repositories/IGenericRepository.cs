using System.Linq.Expressions;

namespace Repositories;

public interface IGenericRepository<TEntity, in TId> where TEntity : class where TId : struct
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    ValueTask<bool> AnyAsync(TId id);
    ValueTask<TEntity?> GetByIdAsync(int id);
    ValueTask AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}