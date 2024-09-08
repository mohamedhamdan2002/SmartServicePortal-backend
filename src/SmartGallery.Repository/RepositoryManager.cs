using Microsoft.Extensions.DependencyInjection;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Repositories;
using SmartGallery.Repository.Data;
using System.Collections;

namespace SmartGallery.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _appDbContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly Hashtable _repositories = new();
        public RepositoryManager(
                AppDbContext appDbContext,
                IServiceProvider serviceProvider
            )
        {
            _appDbContext = appDbContext;
            _serviceProvider = serviceProvider;
        }
        public IRepository<Service> ServiceRepository => GetRepository<Service>();

        public IRepository<Category> CategoryRepository => GetRepository<Category>();

        public IRepository<Reservation> ReservationRepository => GetRepository<Reservation>();

        public IRepository<Review> ReviewRepository => GetRepository<Review>();

        private IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = _serviceProvider.GetRequiredService<IRepository<TEntity>>();
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IRepository<TEntity>;
        }
        public async Task SaveChangesAsync()
            => await _appDbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _appDbContext.DisposeAsync();
    }
}
