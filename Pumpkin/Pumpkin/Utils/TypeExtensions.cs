using System;
using System.Collections;
using System.Collections.Generic;

namespace Pumpkin.Utils
{
    public static class TypeExtensions
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

        public static bool IsEnumurable(this Type type)
        {
            var obj = type as IEnumerable;

            return obj != null;
        }

        public static bool IsList(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)));
        }
    }
}