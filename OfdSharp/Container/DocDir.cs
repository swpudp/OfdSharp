using OfdSharp.Core.Annotation;
using OfdSharp.Core.Basic.Res;
using System;
using System.IO;
using System.Xml;

namespace OfdSharp.Container
{
    /// <summary>
    /// 文档容器
    /// </summary>
    public class DocDir : VirtualContainer
    {
        /// <summary>
        /// 文档容器名称前缀
        /// </summary>
        public const string DocContainerPrefix = "Doc_";

        /// <summary>
        /// 文档的根节点描述文件名称
        /// </summary>
        private const string DocumentFileName = "Document.xml";

        /// <summary>
        /// 文档公共资源索引描述文件名称
        /// </summary>
        private const string PublicResFileName = "PublicRes.xml";

        /// <summary>
        /// 文档自身资源索引描述文件名称
        /// </summary>
        private const string DocumentResFileName = "DocumentRes.xml";

        /// <summary>
        /// 注释入口文件名称
        /// </summary>
        private const string AnnotationsFileName = "Annotations.xml";

        /// <summary>
        /// 附件入口文件名称
        /// </summary>
        public static string Attachments = "Attachments.xml";

        /// <summary>
        /// 表示第几份文档，从0开始
        /// </summary>
        private int _index;

        public DocDir(DirectoryInfo fullDir) : base(fullDir)
        {
            // 标准的签名目录名为 Sign_N (N代表第几个签名)
            String indexStr = fullDir.FullName.Replace(DocContainerPrefix, "");
            _index = int.TryParse(indexStr, out int idx) ? idx : 0;

        }

        /// <summary>
        /// 获取文档的根节点
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetDocument()
        {
            return GetDocument(DocumentFileName);
        }

        /// <summary>
        /// 获取文档自身资源索引对象
        /// </summary>
        /// <returns></returns>
        public Res GetDocumentRes()
        {
            XmlDocument obj = GetDocument(DocumentResFileName);
            return new Res(obj);
        }

        /// <summary>
        /// 获取文档公共资源索引
        /// </summary>
        /// <returns></returns>
        public Res GetPublicRes()
        {
            XmlDocument document = GetDocument(PublicResFileName);
            return new Res(document);
        }

        /// <summary>
        /// 获取注释列表对象    
        /// </summary>
        /// <returns></returns>
        public Annotations GetAnnotations()
        {
            XmlDocument obj = GetDocument(AnnotationsFileName);
            return new Annotations(obj);
        }

        /// <summary>
        /// 获取资源容器
        /// </summary>
        /// <returns></returns>
        public ResDir GetRes()
        {
            return new ResDir(new DirectoryInfo("Res"));
        }
    }
}
