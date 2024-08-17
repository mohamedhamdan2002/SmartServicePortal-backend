using Microsoft.EntityFrameworkCore;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Repository
{
    public static class SpecificationEvaluator
    {
        private static bool hasSelector = false;
        public static IQueryable<TResult> GetQuery<TEntity, TResult>(this IQueryable<TEntity> inputQuery, ISpecification<TEntity, TResult> specification)  where TEntity : BaseEntity
        {
            hasSelector = true;
            var query = inputQuery.GetQuery<TEntity>(specification);
            return query.Select(specification.Selector);
        }
        public static IQueryable<TEntity> GetQuery<TEntity>(this IQueryable<TEntity> inputQuery, IBaseSpecification<TEntity> specification) where TEntity : BaseEntity
        {
            var query = inputQuery;

            if (specification is null)
                return query;

            if (specification.Predicate is not null)
            {
                query = query.Where(specification.Predicate);
            }

            query = specification?.Includes.Aggregate(query,
                (currentQuery, newIncludeExpression) => currentQuery.Include(newIncludeExpression));


            if (specification!.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification!.IsPaginationEnabled)
            {
                query = query!.Skip(specification.Skip).Take(specification.Take);
            }


            if (specification.AsNoTracking && !hasSelector)
            {
                query = query!.AsNoTracking();
            }
            return query!;
        }
    }
}
