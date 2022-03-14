namespace OfdSharp.Primitives.Pages.Description.Color
{
    /// <summary>
    /// 轴线延长线方向是否继续绘制
    ///
    /// 可选值为 0、1、2、3
    /// 0：不向两侧继续绘制渐变
    /// 1: 在结束点至起始点延长线方向绘制渐变
    /// 2：在起始点至结束点延长线方向绘制渐变
    /// 3：向两侧延长线方向绘制渐变
    /// 默认值为 0
    /// </summary>
    public enum ExtendType
    {
        /// <summary>
        /// 不向两侧继续绘制渐变
        /// </summary>
        None = 0,

        /// <summary>
        /// 在结束点至起始点延长线方向绘制渐变
        /// </summary>
        EndToStart = 1,

        /// <summary>
        /// 在起始点至结束点延长线方向绘制渐变
        /// </summary>
        StartToEnd = 2,

        /// <summary>
        /// 向两侧延长线方向绘制渐变
        /// </summary>
        Both = 3
    }
}
