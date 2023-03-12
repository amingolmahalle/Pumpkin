using System.Reflection;
using Pumpkin.Domain.Framework.Contracts.Request.DynamicSearchFilter;
using Pumpkin.Domain.Framework.Extensions;

namespace Pumpkin.Domain.Framework.Helpers.DynamicSearchFilter;

public static class Sort
{
    public static IQueryable<TEntity> DynamicOrderBy<TEntity, TQuery>(IQueryable<TEntity> query, TQuery request)
    {
        var filteringModel = request as BaseSearchFilterPayload;

        if (filteringModel?.Orders is not null)
        {
            foreach (var item in GetOrderByItems<TEntity, TQuery>(request))
            {
                query = query.OrderByProperty(item.Item1, item.Item2);
            }
        }

        return query;
    }

    private static IEnumerable<(string, PropertyInfo[])> GetOrderByItems<TEntity, TQuery>(TQuery request)
    {
        var filteringModel = request as BaseSearchFilterPayload;

        List<PropertyInfo> entityProperties = typeof(TEntity).GetPublicPropertiesFromCache().ToList();

        List<KeyValuePair<string, string[]>> subPropertyNames = filteringModel?
            .Orders?
            .Where(e => e.Field.Contains('.'))
            .Select(e => new KeyValuePair<string, string[]>(e.Field, e.Field.Split('.')))
            .ToList();

        if (filteringModel is not null && filteringModel.Orders is not null)
        {
            foreach (var item in filteringModel.Orders)
            {
                List<PropertyInfo> properties = Extensions.Extensions.FindDomainProperties(entityProperties, subPropertyNames, item.Field);

                if (properties is null || properties.Count == 0)
                    continue;

                yield return (item.Order, properties.ToArray());
            }
        }
    }
}