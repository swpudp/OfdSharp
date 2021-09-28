using System;
using System.Collections.Generic;
using System.Text;

namespace OfdSharp.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 解析枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? default(T) : (T)Enum.Parse(typeof(T), value);
        }
    }
}
