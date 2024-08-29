using Microsoft.AspNetCore.Identity;
using SmartGallery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Core.Specifications
{
    public class BaseSpecification<TEntity> : IBaseSpecification<TEntity> where TEntity : IEntity
    {
        public Expression<Func<TEntity, bool>>? Predicate { get; set; }

        public ICollection<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();

        public Expression<Func<TEntity, object>>? OrderBy { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
        public bool AsNoTracking { get; set; } = true;
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<TEntity, bool>> predicate)
        {
            Predicate = predicate;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> include)
            => Includes.Add(include);
        protected void AddPagination(int pageSize, int pageIndex)
        {
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
            IsPaginationEnabled = true;
        }
    }

}
