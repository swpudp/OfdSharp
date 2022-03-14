using OfdSharp.Primitives.Pages.Object;

namespace OfdSharp.Primitives.Pages.Description.Pattern
{
    /// <summary>
    /// 底纹单元，用底纹填充目标区域时，所使用的单元对象。
    /// CellContent 作为底纹对象的绘制单元，使用一种和外界没有任何关联的独立的坐标空间：
    /// 坐上角（0,0）为原点，X 轴向右增长，Y 轴向下增长，单位为毫米。
    /// </summary>
    public class CellContent
    {
        /// <summary>
        /// 引用资源文件中缩略图图像的标识符
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// 一个页块中可以嵌套其他页块，可含有0到多个页块
        /// </summary>
        public PageBlock PageBlock { get; set; }
    }
}
