using Domain.Entities;
using Domain.Specifications;

namespace Domain.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<int> CountAsync(Specification<TEntity> specification);
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(SpecificationWithResultType<TEntity, TResult> specification);
    Task<TResult?> GetBySpecAsync<TResult>(SpecificationWithResultType<TEntity, TResult> specification);
    Task<TEntity?> GetByIdAsync(int id);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
