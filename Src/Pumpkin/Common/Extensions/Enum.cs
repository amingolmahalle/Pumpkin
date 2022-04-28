using System.ComponentModel;
using System.Globalization;

namespace Pumpkin.Common.Extensions;

public static partial class Extensions
{
    public static string GetDescription<T>(this T e) where T : IConvertible
    {
        string description = null;

        if (e is Enum)
        {
            Type type = e.GetType();
            Array values = Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val) ?? throw new InvalidOperationException());
                    var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (descriptionAttributes.Length > 0)
                    {
                        description = ((DescriptionAttribute) descriptionAttributes[0]).Description;
                    }

                    break;
                }
            }
        }

        return description;
    }
}