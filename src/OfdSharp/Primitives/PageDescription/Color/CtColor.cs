using OfdSharp.Primitives.PageDescription.Pattern;

namespace OfdSharp.Primitives.PageDescription.Color
{
    /// <summary>
    /// 基本颜色：
    /// 本标准中定义的颜色是一个广义的概念，包括基本颜色、底纹和渐变，
    /// 基本颜色支持两种指定方式：一种是通过设定颜色个通道值指定颜色空间的某个颜色，
    /// 另一种是通过索引值取得颜色空间中的一个预定义颜色。
    /// 由于不同颜色空间下，颜色通道的含义、数目各不相同，因此对颜色空间的类型、颜色值的
    /// 描述格式等作出了详细的说明，见表 27。BitsPerComponent（简称 BPC）由效时，
    /// 颜色通道值的取值下限是 0，上限由 BitsPerComponent 决定，取值区间 [0, 2^BPC - 1]
    /// 内的整数，采用 10 进制或 16 进制的形式表示，采用 16 进制表示时，应以“#”加以标识。
    /// 当颜色通道的值超出了相应区间，则按照默认颜色来处理。
    /// </summary>
    public class CtColor
    {
        /// <summary>
        /// 颜色值,指定了当前颜色空间下各通道的取值。
        /// Value的取值应符合"通道1通道2通道3..."格式。
        /// 此属性不出现时,应采用Index属性从颜色空间的调色板中的取值。
        /// 当二者都不出现时,该颜色各通道的值全部为0
        /// </summary>
        public Array Value { get; set; }

        /// <summary>
        /// 调色板中颜色的编号,非负整数,将从当前颜色空间的调色板中取出相应索引的预定义颜色用来绘制。索引从0开始
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 引用资源文件中颜色空间的标识默认值为文档设定的颜色空间
        /// </summary>
        public RefId ColorSpace { get; set; }

        /// <summary>
        /// 颜色透明度,在0~255之间取值。默认为255,表示完全不透明。
        /// </summary>
        public int Alpha { get; set; }

        /// <summary>
        /// 底纹填充,复杂颜色的一种。
        /// </summary>
        public CtPattern Pattern { get; set; }

        /// <summary>
        /// 轴向渐变,复杂颜色的一种。
        /// </summary>
        public AxialShading AxialShd { get; set; }

        /// <summary>
        /// 径向渐变,复杂颜色的一种。
        /// </summary>
        public RadialShading RadialShd { get; set; }

        /// <summary>
        /// 高洛德渐变,复杂颜色的一种。
        /// </summary>
        public RadialShading GouraudShd { get; set; }

        /// <summary>
        /// 格构高洛德渐变,复杂颜色的一种。
        /// </summary>
        public RadialShading LaGouraudShd { get; set; }
    }
}
