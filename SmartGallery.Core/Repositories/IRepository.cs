using SmartGallery.Core.Entities;
using SmartGallery.Core.Specifications;
using System.Linq.Expressions;

namespace SmartGallery.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> CountAsync(IBaseSpecification<TEntity> specification);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(ISpecification<TEntity, TResult> specification);
        Task<TResult?> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification);
        Task<TEntity?> GetByIdAsync(int id);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
