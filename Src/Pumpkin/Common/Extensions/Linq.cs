using System.Linq.Expressions;

namespace Pumpkin.Common.Extensions;

public static partial class Extensions
{
    public static Expression<Func<TEntity, bool>> IdentityEquality<TEntity, TKey>(this TKey id)
    {
        var type = typeof(TEntity);
        var parameter = Expression.Parameter(type, "p");
        var propertyExp = Expression.Property(parameter, "Id");
        var equalExp = Expression.Equal(propertyExp, Expression.Constant(id));

        return Expression.Lambda<Func<TEntity, bool>>(equalExp, parameter);
    }
}