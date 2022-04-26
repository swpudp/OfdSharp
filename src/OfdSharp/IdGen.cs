using OfdSharp.Primitives;
using System.Threading;

namespace OfdSharp
{
    internal class IdGen
    {
        private volatile int _maxId;
        private volatile int _maxSignId;

        public CtId NewId()
        {
            Interlocked.Increment(ref _maxId);
            return new CtId(_maxId);
        }

        /// <summary>
        /// 最大Id
        /// </summary>
        public CtId MaxId => new CtId(_maxId);

        /// <summary>
        /// 生成新的签名id
        /// </summary>
        /// <returns></returns>
        public string NewSignId()
        {
            Interlocked.Increment(ref _maxSignId);
            return $"s{_maxSignId:d3}";
        }

        /// <summary>
        /// 最大编号
        /// </summary>
        public string MaxSignId => $"s{_maxSignId:d3}";
    }
}