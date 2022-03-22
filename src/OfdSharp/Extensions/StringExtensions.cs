using System;
using System.Collections.Generic;
using System.Text;

namespace OfdSharp.Extensions
{
    internal static class StringExtensions
    {
        public static bool NotNullOrEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
