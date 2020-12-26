using System.ComponentModel;
using System.Globalization;

namespace System
{
    public static class StringExtensions
    {
        public static T ConvertTo<T>(this string value) where T: struct, IComparable<T>
        {
            try
            {
                if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
                }

                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return default;
            }
        }
    }
}
