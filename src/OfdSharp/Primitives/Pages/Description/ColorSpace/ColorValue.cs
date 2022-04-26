namespace OfdSharp.Primitives.Pages.Description.ColorSpace
{
    /// <summary>
    /// 调色板中预定义的颜色
    /// 调色板中颜色的索引编号从 0 开始
    /// </summary>
    public class ColorValue
    {
        /// <summary>
        /// 颜色表示：
        /// Gray - 通过一个通道来表明灰度值；例如 "#FF 255"
        /// RGB - 包含3个通道，一次是红、绿、蓝；例如 "#11 #22 #33"、"17 34 51"
        /// CMYK - 包含4个通道，依次是青、黄、品红、黑；例如 "#11 #22 #33 # 44"、"17 34 51 68"
        /// </summary>
        public CtArray Color { get; set; }
    }
}
