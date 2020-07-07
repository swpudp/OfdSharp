using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using OfdSharp.Container;

namespace OfdSharp.Reader
{
    /// <summary>
    /// ofd文件读取器
    /// </summary>
    public class OfdReader
    {
        /// <summary>
        /// ofd文件信息
        /// </summary>
        public FileInfo FileInfo { get; }

        /// <summary>
        /// 文档概述
        /// </summary>
        public DocSummary Summary { get; set; }

        /// <summary>
        /// 压缩文件
        /// </summary>
        private ZipArchive _archive;

        /// <summary>
        /// OFD虚拟容器对象
        /// </summary>
        private OfdDir ofdDir;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath"></param>
        public OfdReader(string filePath)
        {
            FileInfo = new FileInfo(filePath);
            UnZip();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileInfo"></param>
        public OfdReader(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
            UnZip();
        }

        /// <summary>
        /// 确保文件存在
        /// </summary>
        private void EnsureFileExist()
        {
            if (!FileInfo.Exists)
            {
                throw new FileNotFoundException($"文件{FileInfo.Name}未找到");
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        private void UnZip()
        {
            EnsureFileExist();
            FileStream fileStream = File.OpenRead(FileInfo.FullName);
            fileStream.Position = 0;
            _archive = new ZipArchive(fileStream, ZipArchiveMode.Read, true);
            ofdDir = new OfdDir(new DirectoryInfo(FileInfo.FullName));
        }

        /// <summary>
        /// 获取文档内容
        /// </summary>
        /// <returns></returns>
        public string GetBody()
        {
            ZipArchiveEntry entry = _archive.Entries.FirstOrDefault(f => f.Name == "OFD.xml");
            if (entry == null)
            {
                return default;
            }
            MemoryStream memory = new MemoryStream();
            Stream entryStream = entry.Open();
            entryStream.CopyToAsync(memory);
            memory.Position = 0;

            XmlDocument body = new XmlDocument();
            body.Load(memory);


            XmlNamespaceManager ns = new XmlNamespaceManager(body.NameTable);
            ns.AddNamespace("fp", "http://www.edrm.org.cn/schema/e-invoice/2019");

            XmlNode node = body.SelectSingleNode("DocBody/Signatures", ns);

            memory.Dispose();
            entryStream.Dispose();

            return node?.Value;
        }

        private static XmlDocument LoadXml(ZipArchiveEntry entry)
        {
            using (Stream entryStream = entry.Open())
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    entryStream.CopyToAsync(memory);
                    memory.Position = 0;
                    XmlDocument body = new XmlDocument();
                    body.Load(memory);
                    return body;
                }
            }
        }

        /// <summary>
        /// 获取签名文件
        /// </summary>
        /// <returns></returns>
        public string GetSignatures()
        {
            ZipArchiveEntry entry = _archive.Entries.FirstOrDefault(f => f.FullName == "OFD.xml");
            XmlDocument body = LoadXml(entry);

            XmlNamespaceManager ns = new XmlNamespaceManager(body.NameTable);
            ns.AddNamespace("ofd", "http://www.ofdspec.org/2016");

            XmlNode node = body.LastChild.LastChild.LastChild.LastChild;
            return node?.Value;
        }


        public string GetSignedList()
        {
            string signatures = GetSignatures();
            ZipArchiveEntry entry = _archive.Entries.FirstOrDefault(f => f.FullName == signatures);
            if (entry == null)
            {
                return string.Empty;
            }
            XmlDocument document = LoadXml(entry);
            XmlNode node = document.LastChild.LastChild;
            if (node == null)
            {
                return string.Empty;
            }
            ZipArchiveEntry signedEntry = _archive.Entries.FirstOrDefault(f => f.FullName == node.Value);
            XmlDocument signedXml = LoadXml(signedEntry);
            XmlNode signedNode = signedXml.LastChild.LastChild;
            if (signedNode == null)
            {
                return string.Empty;
            }
            foreach (XmlElement childNode in signedNode.ChildNodes)
            {
                return childNode.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取文档
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetDoc(int index)
        {
            return $"Doc_{index}";
        }
    }
}
