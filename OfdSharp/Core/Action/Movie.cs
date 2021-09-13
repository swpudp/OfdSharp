using OfdSharp.Primitives;
using System.Xml;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 播放视频
    /// </summary>
    public class Movie 
    {
        /// <summary>
        /// 引用资源文件中定义的视频资源标识
        /// </summary>
        public RefId ResourceId { get; }

        /// <summary>
        /// 放映参数
        /// 默认Play
        /// </summary>
        public PlayType Operator { get; }
    }
}
