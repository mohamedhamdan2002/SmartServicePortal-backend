using SmartGallery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Core.Specifications
{
    public interface ISpecification<TEntity, TResult> : IBaseSpecification<TEntity> where TEntity : IEntity
    {
        Expression<Func<TEntity, TResult>>? Selector { get; set; }
    }
}
