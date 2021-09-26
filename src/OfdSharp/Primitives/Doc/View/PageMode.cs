namespace OfdSharp.Primitives.Doc.View
{
    /// <summary>
    /// 窗口模式
    /// </summary>
    public enum PageMode
    {
        /// <summary>
        /// 常规模式
        /// </summary>
        None,

        /// <summary>
        /// 开开后全文显示
        /// </summary>
        FullScreen,

        /// <summary>
        /// 同时呈现文档大纲
        /// </summary>
        UseOutlines,

        /// <summary>
        /// 同时呈现缩略图
        /// </summary>
        UseThumbs,

        /// <summary>
        /// 同时呈现语义结构
        /// </summary>
        UseCustomTags,

        /// <summary>
        /// 同时呈现图层
        /// </summary>
        UseLayers,

        /// <summary>
        /// 同时呈现附件
        /// </summary>
        UseAttachments,

        /// <summary>
        /// 同时呈现书签
        /// </summary>
        UseBookmarks
    }
}
