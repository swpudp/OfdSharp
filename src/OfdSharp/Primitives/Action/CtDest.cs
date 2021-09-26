namespace OfdSharp.Primitives.Action
{
    /// <summary>
    /// 目标区域
    /// </summary>
    public class CtDest
    {
        /// <summary>
        /// 声明目标区域的描述方法
        /// </summary>
        public DestType Type { get; set; }

        /// <summary>
        /// 引用跳转目标页面的标识
        /// </summary>
        public RefId PageId { get; set; }

        /// <summary>
        /// 目标区域左上角x坐标
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// 目标区域右下角x坐标
        /// </summary>
        public double Right { get; set; }

        /// <summary>
        /// 目标区域左上角y坐标
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// 目标区域右下角y坐标
        /// </summary>
        public double Bottom { get; set; }

        /// <summary>
        /// 目标区域页面缩放比例,为0或不出现则按照当前缩放比例跳转,可取值范围[0.1,64.0]
        /// </summary>
        public double Zoom { get; set; }
    }
}
