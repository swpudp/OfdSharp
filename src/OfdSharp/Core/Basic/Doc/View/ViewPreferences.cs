namespace OfdSharp.Core.Basic.Doc.View
{
    /// <summary>
    /// 视图首选项
    /// 本标准支持设置文档视图首选项（VPreferences）节点，以达到限定文档初始化视图
    /// 便于阅读的目的。
    /// </summary>
    public class ViewPreferences
    {
        /// <summary>
        /// 窗口模式
        /// </summary>
        public PageMode PageMode { get; set; }

        /// <summary>
        /// 页面布局模式
        /// </summary>
        public PageLayout PageLayout { get; set; }

        /// <summary>
        /// 标题栏显示模式
        /// </summary>
        public TabDisplay TabDisplay { get; set; }

        /// <summary>
        /// 是否隐藏工具栏
        /// </summary>
        public bool HideToolBar { get; set; }

        /// <summary>
        /// 是否隐藏菜单栏
        /// </summary>
        public bool HideMenuBar { get; set; }

        /// <summary>
        /// 是否隐藏主窗口之外的其他窗口组件
        /// </summary>
        public bool HideWindow { get; set; }

        /// <summary>
        /// 文档自动缩放模式
        /// </summary>
        public ZoomMode ZoomMode { get; set; }

        /// <summary>
        /// 文档的缩放率
        /// </summary>
        public double Zoom { get; set; }
    }
}
