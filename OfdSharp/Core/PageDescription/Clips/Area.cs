using System.Xml;
using OfdSharp.Core.BaseType;

namespace OfdSharp.Core.PageDescription.Clips
{
    /// <summary>
    /// 裁剪区域
    /// </summary>
    public class Area : OfdElement
    {
        public Area(XmlDocument xmlDocument) : base(xmlDocument, "Area")
        {
        }

        /// <summary>
        /// 引用资源文件中的绘制参数的标识
        /// 线宽、结合点和端点样式等绘制特性对裁剪效果会产生影响，
        /// 有关绘制参数的描述见 8.2
        /// </summary>
        public string DrawParam { get; set; }

        /// <summary>
        /// 变换矩阵
        /// </summary>
        public StArray Ctm { get; set; }
    }
}
