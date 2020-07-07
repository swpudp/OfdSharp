using OfdSharp.Core.BaseType;
using System;
using System.Xml;

namespace OfdSharp.Core.PageDescription.Color
{
    /// <summary>
    /// 基本颜色
    ///
    /// 本标准中定义的颜色是一个广义的概念，包括基本颜色、底纹和渐变
    /// 基本颜色支持两种指定方式：一种是通过设定颜色个通道值指定颜色空间的某个颜色，
    /// 另一种是通过索引值取得颜色空间中的一个预定义颜色。
    ///
    /// 由于不同颜色空间下，颜色通道的含义、数目各不相同，因此对颜色空间的类型、颜色值的
    /// 描述格式等作出了详细的说明，见表 27。BitsPerComponent（简称 BPC）由效时，
    /// 颜色通道值的取值下限是 0，上限由 BitsPerComponent 决定，取值区间 [0, 2^BPC - 1]
    /// 内的整数，采用 10 进制或 16 进制的形式表示，采用 16 进制表示时，应以“#”加以标识。
    /// 当颜色通道的值超出了相应区间，则按照默认颜色来处理。
    /// </summary>
    public class CtColor : OfdElement
    {
        public CtColor() : base(null, null) { }

        public CtColor(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }

        public CtColor(XmlDocument xmlDocument) : base(xmlDocument, "Color")
        {
        }

        public static CtColor Rgb(int r, int g, int b)
        {
            return new CtColor().SetValue(new StArray(r.ToString(), g.ToString(), b.ToString()));
        }

        /// <summary>
        /// 颜色值
        /// 指定了当前颜色空间下各通道的取值。Value 的取值应
        /// 符合"通道 1 通道 2 通道 3 ..."格式。此属性不出现时，
        /// 应采用 Index 属性从颜色空间的调色板中的取值。二者都不
        /// 出现时，改颜色各通道的值全部为 0
        ///
        /// 颜色表示：
        /// Gray - 通过一个通道来表明灰度值；例如 "#FF 255"
        ///
        /// RGB - 包含3个通道，一次是红、绿、蓝；例如 "#11 #22 #33"、"17 34 51"
        ///
        /// CMYK - 包含4个通道，依次是青、黄、品红、黑；例如 "#11 #22 #33 # 44"、"17 34 51 68"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CtColor SetValue(StArray value)
        {
            if (value == null)
            {
                Element.RemoveAttribute("Value");
                return this;
            }
            Element.SetAttribute("Value", value.ToString());
            return this;
        }

        /// <summary>
        /// 调色板中颜色的编号，非负整数
        /// 将从当前颜色空间的调色板中取出相应索引的预定义颜色用来描绘。
        /// 索引从 0 开始
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(int index)
        {
            if (index < 0)
            {
                throw new NotSupportedException("调色板中颜色的编号，必须为非负整数");
            }
            Element.SetAttribute("Index", index.ToString());
        }

        /// <summary>
        /// 引用资源文件中颜色空间的标识
        /// </summary>
        public string ColorSpace { get; set; }

        /// <summary>
        /// 颜色透明度
        /// 范围在 0~255 之间取值。
        /// 默认为 255，完全不透明。
        /// </summary>
        public int Alpha { get; set; }
    }
}
