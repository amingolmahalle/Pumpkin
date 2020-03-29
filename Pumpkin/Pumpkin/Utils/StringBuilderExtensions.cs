using System.Text;

namespace Pumpkin.Core.Utils
{
    public static class StringBuilderExtensions
    {
        public static void AppendWithSpace(this StringBuilder sb, string item)
        {
            sb.Append(" ");
            sb.Append(item);
            sb.Append(" ");
        }
    }
}