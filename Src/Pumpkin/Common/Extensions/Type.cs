namespace Pumpkin.Common.Extensions;

public static partial class Extensions
{
    public static bool IsComplexType(this Type type)
    {
        return !type.IsPrimitive && type != typeof(string);
    }

    public static bool IsNumeric(this Type type)
    {
        return type == typeof(int) ||
               type == typeof(short) ||
               type == typeof(byte) ||
               type == typeof(long) ||
               type == typeof(decimal) ||
               type == typeof(float) ||
               type == typeof(double);
    }

    public static bool IsString(this Type type)
    {
        return type == typeof(string) || type == typeof(char);
    }

    public static bool IsDateTime(this Type type)
    {
        return (
            type == typeof(DateTime)
        );
    }

    public static bool IsList(this Type type)
    {
        return (type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)));
    }

    public static bool IsAssignableFromGeneric(this Type type, Type genericInterfaceType)
    {
        return type.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType);
    }
}