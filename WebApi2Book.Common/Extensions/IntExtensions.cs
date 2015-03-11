using System;

namespace WebApi2Book.Common.Extensions
{
    public static class IntExtensions
    {
        public static int GetBoundedValue(this int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static int GetBoundedValue(this int? value, int defaultValue, int min)
        {
            var valToBound = value ?? defaultValue;
            return Math.Max(valToBound, min);
        }

        public static int GetBoundedValue(this int? value, int defaultValue, int min, int max)
        {
            var valToBound = value ?? defaultValue;
            return GetBoundedValue(valToBound, min, max);
        }
    }
}
