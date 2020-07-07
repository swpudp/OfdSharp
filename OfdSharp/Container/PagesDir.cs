using System;
using System.IO;

namespace OfdSharp.Container
{
    /// <summary>
    /// 页面容器
    /// </summary>
    public class PagesDir:VirtualContainer
    {
        /// <summary>
        /// 最大页面索引 + 1
        /// index + 1
        /// </summary>
        private int _maxPageIndex;

        public PagesDir(DirectoryInfo fullDir) : base(fullDir)
        {
            InitContainer();
        }

        private void InitContainer()
        {
            //File fullDirFile = new File(getSysAbsPath());
            //File[] files = fullDirFile.listFiles();
            //if (files != null)
            //{
            //    // 遍历容器中已经有的页面目录，初始页面数量
            //    for (File f : files)
            //    {
            //        // 签名目录名为： Page_N
            //        if (f.getName().startsWith(PageDir.PageContainerPrefix))
            //        {
            //            String numb = f.getName().replace(PageDir.PageContainerPrefix, "");
            //            int num = Integer.parseInt(numb);
            //            if (maxPageIndex <= num)
            //            {
            //                maxPageIndex = num + 1;
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 创建一个新的页面容器
        /// </summary>
        /// <returns></returns>
        public PageDir NewPageDir()
        {
            string name = PageDir.PageContainerPrefix + _maxPageIndex;
            _maxPageIndex++;
            // 创建容器
            return new PageDir(new DirectoryInfo(name));
        }

        /// <summary>
        /// 获取索引的页面容器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PageDir GetByIndex(int index)
        {
            string containerName = PageDir.PageContainerPrefix + index;
            return new PageDir(new DirectoryInfo(containerName));
    }
}
}
