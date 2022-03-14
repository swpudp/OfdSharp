namespace OfdSharp.Primitives.Pages.Description.DrawParam
{
    /// <summary>
    /// 线条连接样式：指定了两个线的端点结合时采用的样式
    /// 线条连接样式的取值和显示效果之间的关系见表 22
    /// </summary>
    public enum LineJoinType
    {
        /// <summary>
        /// 斜接
        /// </summary>
        Miter,

        /// <summary>
        /// 弧形的
        /// </summary>
        Round,

        /// <summary>
        /// 斜面
        /// </summary>
        Bevel
    }
}
