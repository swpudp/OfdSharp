using System.IO;
using System.Xml;
using OfdSharp.Core.Annotation;
using OfdSharp.Core.Basic.PageObject;
using OfdSharp.Core.Basic.Resources;

namespace OfdSharp.Container
{
    /// <summary>
    /// 页面目录容器
    /// </summary>
    public class PageDir : VirtualContainer
    {
        /// <summary>
        /// 页面容器名称前缀
        /// </summary>
        public static string PageContainerPrefix = "Page_";

        /// <summary>
        /// 页面描述文件名称
        /// </summary>
        public static string ContentFileName = "Content.xml";

        /// <summary>
        /// 记录了资源描述文件名称
        /// </summary>
        public static string PageResFileName = "PageRes.xml";

        /// <summary>
        /// 记录了页面关联的注解对象
        /// </summary>
        public static string AnnotationFileName = "Annotation.xml";

        /// <summary>
        /// 代表OFD中第几页
        /// index 从 0 开始取
        /// </summary>
        private int _index;

        public PageDir(DirectoryInfo fullDir) : base(fullDir)
        {
            string indexStr = fullDir.FullName.Replace(PageContainerPrefix, "");
            _index = int.TryParse(indexStr, out int idx) ? idx : 0;
        }

        /// <summary>
        /// 获取页面资源描述文件
        /// </summary>
        /// <returns></returns>
        public Resource GetPageRes()
        {
            XmlDocument obj = GetDocument(PageResFileName);
            return new Resource();
        }

        /// <summary>
        /// 获取分页注释文件
        /// </summary>
        /// <returns></returns>
        public AnnotationPage GetPageAnnotation()
        {
            XmlDocument obj = GetDocument(AnnotationFileName);
            return new AnnotationPage(obj, "", "");
        }

        /// <summary>
        /// 获取页面资源目录
        /// 如果目录不存在则创建
        /// </summary>
        /// <returns></returns>
        public ResDir GetRes()
        {
            return new ResDir(new DirectoryInfo("Res"));
        }

        /// <summary>
        /// 获取页面描述对象
        /// </summary>
        /// <returns></returns>
        public Page GetContent()
        {
            XmlDocument obj = GetDocument(ContentFileName);
            return new Page();
        }
    }
}
