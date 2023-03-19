using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Framework.Specifications;

namespace Pumpkin.Infrastructure.Framework.Data.Specifications
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);
            
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}