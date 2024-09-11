using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Specifications;

public abstract class SpecificationWithResultType<TEntity, TResult> : Specification<TEntity> where TEntity : IEntity
{

    public Expression<Func<TEntity, TResult>>? Selector { get; set; }


    public SpecificationWithResultType()
    {

    }
    public SpecificationWithResultType(Expression<Func<TEntity, bool>> predicate) : base(predicate)
    {

    }


    public SpecificationWithResultType(Expression<Func<TEntity, TResult>> selector)
    {
        Selector = selector;
    }
    public SpecificationWithResultType(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector) : base(predicate)
    {
        Selector = selector;
    }

}