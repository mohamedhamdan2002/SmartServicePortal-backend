using SmartGallery.Core.Entities;
using SmartGallery.Core.Specifications;

namespace SmartGallery.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> CountAsync(Specification<TEntity> specification);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(SpecificationWithResultType<TEntity, TResult> specification);
        Task<TResult?> GetBySpecAsync<TResult>(SpecificationWithResultType<TEntity, TResult> specification);
        Task<TEntity?> GetByIdAsync(int id);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
