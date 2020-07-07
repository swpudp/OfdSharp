using System.Xml;

namespace OfdSharp.Core.Basic.PageObj.Layer.Block
{

    /// <summary>
    /// 图像对象
    ///
    /// 带有播放视频动作时，见第 12 章
    /// </summary>
    public class ImageObject : PageBlockType
    {
        public ImageObject(XmlDocument xmlDocument) : base(xmlDocument, "ImageObject")
        {
        }

        public string Id { get; set; }
    }
}
