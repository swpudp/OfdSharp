namespace OfdSharp.Primitives.Pages.Object
{
    /// <summary>
    /// 图层类型
    /// 统称类型分为前景层、正文层、背景层，这些层按照出现的
    /// 先后顺序依次进行渲染，每一层的默认颜色采用透明。
    /// 层的渲染顺序如下图 （图 16 图层渲染顺序）
    /// </summary>
    public enum LayerType
    {
        /// <summary>
        /// 前景层
        /// </summary>
        Foreground,

        /// <summary>
        /// 正文层
        /// </summary>
        Body,

        /// <summary>
        /// 背景层
        /// </summary>
        Background
    }
}
