using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#pragma warning disable 8632

namespace GarlicBread.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasCustomAttribute<TAttributeType>(this Type type,
            [NotNullWhen(true)] out TAttributeType? attribute) where TAttributeType : Attribute
        {
            var attr = type.GetCustomAttributes(typeof(TAttributeType), true).FirstOrDefault();
            if (attr != null)
            {
                attribute = (TAttributeType) attr;
                return true;
            }

            attribute = default;
            return false;
        }

        public static bool HasCustomAttribute<TAttributeType>(this Type type) where TAttributeType : Attribute
        {
            return HasCustomAttribute<TAttributeType>(type, out _);
        }
    }
}