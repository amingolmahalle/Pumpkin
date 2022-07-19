using System.Reflection;
using Framework.ValueObjects;

namespace Domain.Framework.Extensions;

public static partial class Extensions
{
    private static readonly object Locker = new();

    private static readonly Dictionary<Type, List<PropertyInfo>> ObjectProperties = new();

    public static IEnumerable<PropertyInfo> GetPublicPropertiesFromCache(this Type type)
    {
        List<PropertyInfo> properties;

        lock (Locker)
        {
            if (!ObjectProperties.TryGetValue(type, out properties))
            {
                properties = type.GetPublicProperties();

                lock (Locker)
                {
                    if (!ObjectProperties.ContainsKey(type))
                        ObjectProperties.Add(type, properties);
                }
            }
        }

        return properties;
    }

    public static List<PropertyInfo> GetPublicProperties(this Type type)
    {
        if (type.IsInterface)
        {
            var propertyInfos = new List<PropertyInfo>();

            var considered = new List<Type>();
            var queue = new Queue<Type>();

            considered.Add(type);
            queue.Enqueue(type);

            while (queue.Count > 0)
            {
                var subType = queue.Dequeue();

                foreach (var subInterface in subType.GetInterfaces())
                {
                    if (considered.Contains(subInterface))
                        continue;

                    considered.Add(subInterface);
                    queue.Enqueue(subInterface);
                }

                var typeProperties = subType.GetProperties(
                    BindingFlags.FlattenHierarchy
                    | BindingFlags.Public
                    | BindingFlags.Instance);

                var newPropertyInfos = typeProperties
                    .Where(x => !propertyInfos.Contains(x));

                propertyInfos.InsertRange(0, newPropertyInfos);
            }

            return propertyInfos.ToList();
        }

        return type
            .GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance)
            .ToList();
    }

    public static object ChangeType(object value, Type conversion)
    {
        if (value is null || conversion == null)
            return null;

        if (conversion.IsGenericType && conversion.GetGenericTypeDefinition() == typeof(Nullable<>))
            conversion = Nullable.GetUnderlyingType(conversion);
        if (conversion == null)
            return null;

        if (conversion.Name == nameof(EntityUuid))
            value = EntityUuid.FromString(value.ToString());
        else if (conversion.Name == nameof(DateTime))
            value = long.Parse(value.ToString()!).ToUtcDateTime();
        else if (conversion is {IsEnum: true})
            value = Enum.Parse(conversion, value.ToString()!);

        return Convert.ChangeType(value, conversion);
    }

    public static List<PropertyInfo> FindDomainProperties(
        List<PropertyInfo> entityProperties,
        List<KeyValuePair<string, string[]>> subPropertyNames,
        string propertyName)
    {
        var finalProperty = new List<PropertyInfo>();

        var domainProperty = entityProperties?
            .Where(e => string.Equals(e.Name, propertyName, StringComparison.OrdinalIgnoreCase))?
            .SingleOrDefault();

        if (domainProperty is null)
        {
            if (subPropertyNames.Any(e => e.Key == propertyName))
            {
                foreach (var item in subPropertyNames?.Single(e => e.Key == propertyName).Value)
                {
                    domainProperty = entityProperties?
                        .Where(e => string.Equals(e.Name, item, StringComparison.OrdinalIgnoreCase))
                        .SingleOrDefault();

                    if (domainProperty is not null && (!domainProperty.PropertyType.IsClass || domainProperty.PropertyType.GetMethods().Any(m => m.Name == "<Clone>$")))
                    {
                        finalProperty.Add(domainProperty);

                        return finalProperty;
                    }

                    if (domainProperty?.PropertyType.IsClass == true && domainProperty.PropertyType != typeof(string))
                    {
                        var subEntityProperties = domainProperty
                            .PropertyType
                            .GetPublicPropertiesFromCache()?
                            .ToList();

                        var prevProperties = new List<PropertyInfo>();

                        prevProperties.Add(domainProperty);
                        prevProperties.AddRange(FindDomainProperties(subEntityProperties, subPropertyNames, propertyName));

                        return prevProperties;
                    }
                }
            }
            else
                return null;
        }

        finalProperty.Add(domainProperty);

        return finalProperty;
    }
}