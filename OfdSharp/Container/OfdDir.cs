using OfdSharp.Core.Basic.Ofd;
using System.IO;
using System.Xml;

namespace OfdSharp.Container
{
    /// <summary>
    /// OFD文档对象
    /// </summary>
    public class OfdDir : VirtualContainer
    {
        /// <summary>
        /// OFD文档主入口文件名称
        /// </summary>
        public static string OfdFileName = "OFD.xml";

        public OfdDir(DirectoryInfo fullDir) : base(fullDir)
        {
            InitContainer();
        }

        /// <summary>
        /// 最大文档索引 + 1
        /// </summary>
        private int _maxDocIndex;

        private void InitContainer()
        {
            //File fullDirFile = new File(getSysAbsPath());
            //File[] files = fullDirFile.listFiles();
            //if (files != null)
            //{
            //    // 遍历容器中已经有的文档目录，初始文档数量
            //    for (File f : files)
            //    {
            //        // 文档目录名为： Doc_N
            //        if (f.getName().startsWith(DocDir.DocContainerPrefix))
            //        {
            //            String numb = f.getName().replace(DocDir.DocContainerPrefix, "");
            //            int num = Integer.parseInt(numb);
            //            if (maxDocIndex <= num)
            //            {
            //                maxDocIndex = num + 1;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 文档主入口文件对象
        /// </summary>
        /// <returns></returns>
        public Ofd GetOfd()
        {
            XmlDocument obj = GetDocument(OfdFileName);
            return new Ofd(obj);
        }

        /// <summary>
        /// 新建一个文档容器
        /// </summary>
        /// <returns></returns>
        public DocDir NewDoc()
        {
            string name = DocDir.DocContainerPrefix + _maxDocIndex;
            _maxDocIndex++;
            return new DocDir(new DirectoryInfo(name));
        }

        /// <summary>
        /// 获取指定Index的文档
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DocDir GetDoc(int index)
        {
            string name = DocDir.DocContainerPrefix + index;
            if (index >= _maxDocIndex)
            {
                _maxDocIndex = index + 1;
            }
            return new DocDir(new DirectoryInfo(name));
        }
    }
}
