using System.Collections;

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
    }
}
