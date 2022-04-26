namespace OfdSharp.Primitives.Pages.Description.ColorSpace
{
    /// <summary>
    /// 颜色空间
    /// 本标准支持 GRAY、RGB、CMYK 颜色空间。除通过
    /// 设置各通道使用颜色空间内的任意颜色之外，还可
    /// 在颜色空间内定义调色板或指定相应颜色配置文件，
    /// 通过设置索引值进行引用。
    /// </summary>
    public class CtColorSpace
    {
        /// <summary>
        /// 标识
        /// </summary>
        public CtId Id { get; set; }

        /// <summary>
        /// 颜色空间的类型
        /// </summary>
        public ColorSpaceType Type { get; set; } = ColorSpaceType.RGB;

        /// <summary>
        /// 每个颜色通道使用的位数
        /// </summary>
        public BitsPerComponent BitsPerComponent { get; set; } = BitsPerComponent.Bit8;

        /// <summary>
        /// 指向包内颜色配置文件
        /// </summary>
        public CtLocation Profile { get; set; }

        /// <summary>
        /// 调色板
        /// </summary>
        public Palette Palette { get; set; }
    }
}
