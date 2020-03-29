using System.Collections.Generic;
using System.Linq;

namespace Pumpkin.Utils
{
    public static class EnumurableExtensions
    {
        public static bool HasItem<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }
    }
}