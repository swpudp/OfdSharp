namespace OfdSharp.Primitives.PageDescription.Clips
{
    /// <summary>
    /// 裁剪区域
    /// </summary>
    public class Area
    {
        /// <summary>
        /// 引用资源文件中的绘制参数的标识,线宽、结合点和端点样式等绘制特性对裁剪效果会产生影响
        /// </summary>
        public RefId DrawParam { get; set; }

        /// <summary>
        /// 针对对象坐标系,对Area下包含的Path和Text进行进一步的变换
        /// </summary>
        public Array Ctm { get; set; }
    }
}
