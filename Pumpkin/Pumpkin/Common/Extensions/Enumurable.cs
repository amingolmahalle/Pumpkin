using System.Collections.Generic;
using System.Linq;

namespace Pumpkin.Common.Extensions
{
    public static partial class Extensions
    {
        public static bool HasItem<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }
    }
}