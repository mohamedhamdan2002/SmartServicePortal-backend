using SmartGallery.Core.Entities;
using System.Linq.Expressions;

namespace SmartGallery.Core.Specifications
{
    public interface IBaseSpecification<TEntity> where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>>? Predicate { get; set; }
        ICollection<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; set; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
        bool AsNoTracking { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
