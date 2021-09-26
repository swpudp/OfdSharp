namespace OfdSharp.Primitives.Action
{
    /// <summary>
    /// 申明目标区域的描述方法
    /// 表 54 目标区域属性
    /// </summary>
    public enum DestType
    {
        /// <summary>
        /// 目标区域由左上角位置（Left，Top）
        /// 以及页面缩放比例（Zoom）确定
        /// </summary>
        Xyz,

        /// <summary>
        /// 适合整个窗口区域
        /// </summary>
        Fit,

        /// <summary>
        /// 适合窗口宽度，目标区域由Top确定
        /// </summary>
        FitH,

        /// <summary>
        /// 适合窗口高度，目标区域由Left确定
        /// </summary>
        FitV,

        /// <summary>
        /// 适合窗口内的目标区域，目标区域为
        /// （Left，Top，Right，Bottom）所确定的矩形区域
        /// </summary>
        FitR
    }
}
