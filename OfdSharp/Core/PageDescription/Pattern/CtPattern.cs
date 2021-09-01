using OfdSharp.Primitives;
using System.Xml;

namespace OfdSharp.Core.PageDescription.Pattern
{
    /// <summary>
    /// 底纹
    /// </summary>
    public class CtPattern : OfdElement
    {
        public CtPattern(XmlDocument xmlDocument) : base(xmlDocument, "Pattern")
        {
        }

        /// <summary>
        /// 底纹单元宽度
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 底纹单元高度
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        ///  X 方向底纹单元间距
        ///
        /// 默认值为底纹单元的宽度
        /// 若设定值小于底纹单元的宽度时，应按默认值处理
        /// </summary>
        public double XStep { get; set; }

        /// <summary>
        /// 底纹单元的翻转方式
        /// </summary>
        public ReflectMethod ReflectMethod { get; set; }

        /// <summary>
        /// 底纹单元起始位置
        /// </summary>
        public RelativeTo RelativeTo { get; set; }

        /// <summary>
        /// 底纹单元的变换矩阵
        /// 用于某些需要对底纹单元进行平移旋转变换的场合，
        /// 默认为单位矩阵；底纹呈现时先做 XStep、YStep 排列，
        /// 然后一起做 CTM 处理
        /// </summary>
        public Array Ctm { get; set; }

        /// <summary>
        /// 底纹单元
        /// 用底纹填充目标区域时，所使用的单元对象
        /// </summary>
        public CellContent CellContent { get; set; }
    }
}
