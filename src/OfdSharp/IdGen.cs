using OfdSharp.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OfdSharp
{
    internal class IdGen
    {
        private volatile int _maxId;
        private volatile int _maxSignId;

        public Id NewId()
        {
            Interlocked.Increment(ref _maxId);
            return new Id(_maxId);
        }

        /// <summary>
        /// 最大Id
        /// </summary>
        public Id MaxId => new Id(_maxId);

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