using System.Text.RegularExpressions;
using System.Threading;

namespace OfdSharp.Sign
{
    /// <summary>
    /// 自增的签名ID
    /// </summary>
    public class AtomicSignId
    {
        /// <summary>
        /// 根据标准推荐ID样式为
        /// 'sNNN',NNN从1起。
        /// </summary>
        private static readonly Regex IdPattern = new Regex("s(\\d{3})");

        private static int _max;

        public AtomicSignId(string maxSignId)
        {
            _max = Parse(maxSignId);
        }

        /// <summary>
        /// 增长并获取签名ID
        /// </summary>
        /// <returns></returns>
        public string IncrementAndGet()
        {
            Interlocked.Increment(ref _max);
            return $"{_max:D3}";
        }

        /// <summary>
        /// 获取当前签名ID
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            return $"{_max:D3}";
        }

        /// <summary>
        /// 解析出电子签名的ID数字
        /// </summary>
        /// <param name="id">ID字符串</param>
        /// <returns></returns>
        public static int Parse(string id)
        {
            var m = IdPattern.Match(id);
            if (m.Success)
            {
                var idNumStr = m.Groups[1].Value;
                return int.Parse(idNumStr);
            }
            return int.Parse(id);
        }

    }
}
