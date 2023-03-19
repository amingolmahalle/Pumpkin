using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Pumpkin.Domain.Framework.Specifications;

namespace Pumpkin.Domain.Framework.Data.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {
    }

    protected BaseSpecification(Expression<Func<T, bool>> criteria)
        => Criteria = criteria;

    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludesWithThenIncludes { get; } = new();

    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
        => Includes.Add(includeExpression);

    protected void AddIncludeWithThenIncludes(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        => IncludesWithThenIncludes.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        => OrderBy = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        => OrderByDescending = orderByDescExpression;
}