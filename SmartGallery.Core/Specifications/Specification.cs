using SmartGallery.Core.Entities;
using System.Linq.Expressions;

namespace SmartGallery.Core.Specifications
{
    public class Specification<TEntity, TResult> : BaseSpecification<TEntity>, ISpecification<TEntity, TResult> where TEntity : IEntity
    {

        public Expression<Func<TEntity, TResult>>? Selector { get; set; }
 

        public Specification()
        {
            
        }
        public Specification(Expression<Func<TEntity, bool>> predicate) : base(predicate)
        {

        }


        public Specification(Expression<Func<TEntity, TResult>> selector)
        {
            Selector = selector;
        }
        public Specification(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) : base(predicate)
        {
            Selector = selector;
        }
      
    }

}
