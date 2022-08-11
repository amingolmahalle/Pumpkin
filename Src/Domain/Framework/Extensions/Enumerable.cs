namespace Pumpkin.Domain.Framework.Extensions;

public static partial class Extensions
{
    public static bool HasItem<T>(this IEnumerable<T> list)
    {
        return list != null && list.Any();
    }
}