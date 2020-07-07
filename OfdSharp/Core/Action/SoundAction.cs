using System.Xml;

namespace OfdSharp.Core.Action
{
    public class SoundAction : BaseAction
    {
        public SoundAction(XmlDocument xmlDocument, int volume, bool repeat, bool synchronous, long resourceId) : base(xmlDocument, "Sound")
        {
            Volume = volume;
            Repeat = repeat;
            Synchronous = synchronous;
            ResourceId = resourceId;

            Element.SetAttribute("Volume", volume.ToString());
            Element.SetAttribute("Repeat", repeat.ToString());
            Element.SetAttribute("ResourceID", resourceId.ToString());
            Element.SetAttribute("Synchronous", synchronous.ToString());
        }

        /// <summary>
        /// 获取 播放音量，取值范围[0,100]
        /// </summary>
        public int Volume { get; }

        /// <summary>
        /// 设置  此音频是否需要同步播放
        /// 如果此属性为 true，则 Synchronous 值无效
        /// true - 同步； false - 异步
        /// </summary>
        public bool Repeat { get; }

        /// <summary>
        /// 设置 是否同步播放
        /// true 表示后续动作应等待此音频播放结束后才能开始，
        /// false 表示立刻返回并开始下一个动作
        /// </summary>
        public bool Synchronous { get; }

        /// <summary>
        /// 设置 引用资源文件中的音频资源标识符
        /// </summary>
        public long ResourceId { get; }
    }
}
