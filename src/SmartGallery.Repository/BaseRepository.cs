using Microsoft.EntityFrameworkCore;
using SmartGallery.Core;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Repositories;
using SmartGallery.Core.Specifications;
using SmartGallery.Repository.Data;

namespace SmartGallery.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, IEntity
    {
        private readonly AppDbContext _dbContext;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<int> CountAsync(Specification<TEntity> specification)
            => await ApplaySpecifictaions(specification).CountAsync();


        public async Task CreateAsync(TEntity entity)
            => await _dbContext.AddAsync(entity);


        public void Delete(TEntity entity)
            => _dbContext.Remove(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(SpecificationWithResultType<TEntity, TResult> specification)
        {
            return await ApplaySpecifictaions(specification).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task<TResult?> GetBySpecAsync<TResult>(SpecificationWithResultType<TEntity, TResult> specification)
        {
            return await ApplaySpecifictaions(specification).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
        private IQueryable<TResult> ApplaySpecifictaions<TResult>(SpecificationWithResultType<TEntity, TResult> specification)
        {
            return _dbContext.Set<TEntity>().GetQuery(specification);
        }

        private IQueryable<TEntity> ApplaySpecifictaions(Specification<TEntity> specification)
        {
            return _dbContext.Set<TEntity>().GetQuery(specification);
        }
    }
}
