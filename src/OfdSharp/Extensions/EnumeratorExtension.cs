using System.Collections;
using System.Collections.Generic;

namespace OfdSharp.Extensions
{
    /// <summary>
    /// 迭代器扩展
    /// </summary>
    public static class EnumeratorExtension
    {
        /// <summary>
        /// 下一个元素
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static object Next(this IEnumerator enumerator)
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }

        /// <summary>
        /// 包含任何元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsAny<T>(this ICollection<T> collection)
        {
            return collection != null && collection.Count > 0;
        }
    }
}
