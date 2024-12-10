using System.Linq.Expressions;

namespace WebApplicationElevator.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

    Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);

    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
}