using System.Text;

namespace Domain.Framework.Extensions;

public static partial class Extensions
{
    public static void AppendWithSpace(this StringBuilder sb, string item)
    {
        sb.Append(" ");
        sb.Append(item);
        sb.Append(" ");
    }
}