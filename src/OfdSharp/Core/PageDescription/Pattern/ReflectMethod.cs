namespace OfdSharp.Core.PageDescription.Pattern
{
    /// <summary>
    /// 翻转绘制效果
    /// </summary>
    public enum ReflectMethod
    {
        /// <summary>
        /// 普通重复
        /// </summary>
        Normal,

        /// <summary>
        /// 竖轴对称翻转
        /// </summary>
        Column,

        /// <summary>
        /// 横轴对称翻转
        /// </summary>
        Row,

        /// <summary>
        /// 十字轴对称翻转
        /// </summary>
        RowAndColumn
    }
}
