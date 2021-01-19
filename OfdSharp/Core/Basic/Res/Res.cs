using System.Xml;

namespace OfdSharp.Core.Basic.Res
{
  public  class Res:OfdElement
    {
        public Res(XmlDocument xmlDocument) : base(xmlDocument, "Res")
        {
        }

        /// <summary>
        /// 此资源文件的通用数据存储路径
        /// </summary>
        public string BaseLoc { get; set; }
    }
}
