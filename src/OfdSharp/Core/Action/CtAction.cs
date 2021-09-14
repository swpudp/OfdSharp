using OfdSharp.Core.Graph.Tight;

namespace OfdSharp.Core.Action
{
    /// <summary>
    /// 动作
    /// </summary>
    public class CtAction
    {
        /// <summary>
        /// 事件类型,触发动作的条件
        /// </summary>
        public TriggerEvent Event { get; set; }

        /// <summary>
        /// 指定多个复杂区域为该链接对象的启动区域,不出现时以所在图元或页面的外接矩形作为启动区域
        /// </summary>
        public CtRegion Region { get; set; }

        /// <summary>
        /// 本文档内的跳转
        /// </summary>
        public Goto Goto { get; set; }

        /// <summary>
        /// 打开或访问一个URI链接
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// 打开本文档附件
        /// </summary>
        public GotoAttachment GotoA { get; set; }

        /// <summary>
        /// 播放一段音频
        /// </summary>
        public Sound Sound { get; set; }

        /// <summary>
        /// 播放一段视频
        /// </summary>
        public Movie Movie { get; set; }
    }
}
