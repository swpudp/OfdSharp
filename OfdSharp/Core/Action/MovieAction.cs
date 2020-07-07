using System.Xml;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 动作用于播放视频
    /// 图 79 播放视频动作属性
    /// </summary>
    public class MovieAction : BaseAction
    {
        public MovieAction(XmlDocument xmlDocument, string resourceId, PlayType playType) : base(xmlDocument, "Movie")
        {
            ResourceId = resourceId;
            Operator = playType;

            Element.SetAttribute("ResourceID", resourceId);
            Element.SetAttribute("Operator", playType.ToString());
        }

        /// <summary>
        /// 引用资源文件中定义的视频资源标识
        /// </summary>
        public string ResourceId { get; }

        /// <summary>
        /// 放映参数
        /// 默认Play
        /// </summary>
        public PlayType Operator { get; }
    }
}
