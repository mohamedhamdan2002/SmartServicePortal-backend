using Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Abstractions;
using Domain.Interfaces;
namespace Infrastructure.Repositories;
public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, BaseEntity
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }
    public async Task<int> CountAsync(Specification<TEntity> specification)
        => await ApplaySpecifictaions(specification).CountAsync();
    public void Create(TEntity entity)
        => _dbContext.Add(entity);
    public void Delete(TEntity entity)
        => _dbContext.Remove(entity);
    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();
    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Specification<TEntity, TResult> specification)
        => await ApplaySpecifictaions(specification).ToListAsync();
    public async Task<TEntity?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);
    public async Task<TResult?> GetBySpecAsync<TResult>(Specification<TEntity, TResult> specification)
        => await ApplaySpecifictaions(specification).FirstOrDefaultAsync();
    public void Update(TEntity entity)
        => _dbContext.Update(entity);
    public async Task DeleteRangeAsync(IEnumerable<int> ids)
        => await _dbSet.Where(e => ids.Any(id => e.Id == id)).ExecuteDeleteAsync();
    private IQueryable<TResult> ApplaySpecifictaions<TResult>(Specification<TEntity, TResult> specification)
        => _dbSet.GetQuery(specification);
    private IQueryable<TEntity> ApplaySpecifictaions(Specification<TEntity> specification)
        => _dbSet.GetQuery(specification);
    public async Task<TEntity?> GetBySpecAsync(Specification<TEntity> specification)
        => await ApplaySpecifictaions(specification).FirstOrDefaultAsync();

}
