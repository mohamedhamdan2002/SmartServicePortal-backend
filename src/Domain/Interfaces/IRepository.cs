using Domain.Abstractions;
using Domain.Specifications;

namespace Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<int> CountAsync(Specification<TEntity> specification);
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Specification<TEntity, TResult> specification);
    Task<TResult?> GetBySpecAsync<TResult>(Specification<TEntity, TResult> specification);
    Task<TEntity?> GetBySpecAsync(Specification<TEntity> specification);
    Task<TEntity?> GetByIdAsync(int id);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<int> ids);
}
