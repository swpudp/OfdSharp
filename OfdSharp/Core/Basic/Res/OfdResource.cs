using System.Xml;

namespace OfdSharp.Core.Basic.Res
{
    /// <summary>
    /// 资源文件抽象类型
    /// </summary>
    public class OfdResource : OfdElement
    {
        public OfdResource(XmlDocument xmlDocument, string name) : base(xmlDocument, name)
        {
        }
    }
}
