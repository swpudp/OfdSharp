namespace OfdSharp.Primitives.Doc
{
    /// <summary>
    /// 页面区域结构
    /// </summary>
    public class PageArea
    {
        /// <summary>
        /// 页面物理区域，左上角的坐标为页面空间坐标系的原点
        /// </summary>
        public Box Physical { get; set; }

        /// <summary>
        /// 显示区域，页面内容实际显示或打印输出的区域，位于页面物理区域内，包含页眉，页脚、版心等内容
        /// [额外处理]如果显示区域不完全位于页面物理区域内，页面物理区域外的部分被忽略。如果显示区域完全位于页面物理区域外，则该页为空白页
        /// </summary>
        public Box Application { get; set; }

        /// <summary>
        /// 版心区域，即文件的正文区域，位于显示区域内。左上角的坐标决定了其在显示区域内的位置。
        /// [额外处理]如果版心区域不完全位于显示区域内，显示区域外的部分被忽略。如果版心区域完全位于显示区域外，则版心内容不被绘制
        /// </summary>
        public Box Content { get; set; }

        /// <summary>
        /// 出血区域，即超出设备性能限制的额外出血区域，位于页面物理区域外。不出现时，默认值为页面物理区域。
        /// [额外处理]如果出血区域不完全位于页面物理区域外，页面物理区域内的部分被忽略。如果出血区域完全位于页面物理区域内，出血区域无效
        /// </summary>
        public Box Bleed { get; set; }
    }
}
