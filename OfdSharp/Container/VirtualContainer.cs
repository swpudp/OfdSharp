using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace OfdSharp.Container
{
    public class VirtualContainer
    {
        /// <summary>
        /// 文件根路径(完整路径包含当前文件名)
        /// </summary>
        private string _fullPath;

        /// <summary>
        /// 目录名称
        /// </summary>
        private string _name;

        /// <summary>
        /// 所属容器
        /// </summary>
        private VirtualContainer _parent;

        /// <summary>
        /// 文件缓存
        /// </summary>
        private Dictionary<string, XmlDocument> _fileCache;

        /// <summary>
        /// 目录中的虚拟容器缓存
        /// </summary>
        private Dictionary<string, VirtualContainer> _dirCache;

        public string GetContainerName()
        {
            return _name;
        }

        private VirtualContainer()
        {
            _fileCache = new Dictionary<string, XmlDocument>(7);
            _dirCache = new Dictionary<string, VirtualContainer>(5);
            _parent = this;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fullDir"></param>
        public VirtualContainer(DirectoryInfo fullDir)
        {

            if (fullDir == null)
            {
                throw new Exception("完整路径(fullDir)为空");
            }
            // 目录不存在或不是一个目录
            if (!fullDir.Exists)
            {
                try
                {
                    // 创建目录
                    fullDir.Create();
                }
                catch (IOException e)
                {
                    throw new Exception("无法创建指定目录", e);
                }
            }
            _fullPath = fullDir.FullName;
            _name = fullDir.Name;

        }

        protected void SetDocument(string name, XmlDocument document)
        {
            _fileCache.Add(name, document);
        }

        protected XmlDocument GetDocument(string name)
        {
            return _fileCache.TryGetValue(name, out XmlDocument document) ? document : null;
        }
    }
}
