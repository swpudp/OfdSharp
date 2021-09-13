using OfdSharp.Primitives;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 播放音频
    /// </summary>
    public class Sound
    {
        /// <summary>
        /// 引用资源文件中的音频资源标识符
        /// </summary>
        public RefId ResourceId { get; set; }

        /// <summary>
        /// 播放音量，取值范围[0,100]，默认100
        /// </summary>
        public int Volume { get; set; } = 100;

        /// <summary>
        /// 此音频是否需要循环播放，如果此属性为true,则Synchronous值无效,默认为false
        /// </summary>
        public bool Repeat { get; set; }

        /// <summary>
        /// 是否同步播放，
        /// true 表示后续动作应等待此音频播放结束后才能开始，false 表示立刻返回并开始下一个动作，默认值为false
        /// </summary>
        public bool Synchronous { get; set; }
    }
}
