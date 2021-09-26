using OfdSharp.Primitives.Invoice;
using OfdSharp.Primitives.Signature;
using OfdSharp.Utils;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;

namespace OfdSharp.Reader
{
    /// <summary>
    /// ofd文件读取器
    /// </summary>
    public class OfdReader
    {
        /// <summary>
        /// 文档概述
        /// </summary>
        public DocSummary Summary { get; set; }

        /// <summary>
        /// ofd入口文件名称
        /// </summary>
        private const string OfdFileName = "OFD.xml";

        /// <summary>
        /// 压缩文件
        /// </summary>
        private ZipArchive _archive;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath"></param>
        public OfdReader(string filePath)
        {
            UnZip(new FileInfo(filePath));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileInfo"></param>
        public OfdReader(FileInfo fileInfo)
        {
            UnZip(fileInfo);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stream"></param>
        public OfdReader(Stream stream)
        {
            UnZip(stream);
        }

        /// <summary>
        /// 确保文件存在
        /// </summary>
        private static void EnsureFileExist(FileSystemInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException($"文件{fileInfo.Name}未找到");
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        private void UnZip(FileInfo fileInfo)
        {
            EnsureFileExist(fileInfo);
            using (FileStream originalFileStream = fileInfo.OpenRead())
            {
                MemoryStream decompressedStream = new MemoryStream();
                originalFileStream.CopyTo(decompressedStream);
                UnZip(decompressedStream);
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        private void UnZip(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            _archive = new ZipArchive(stream, ZipArchiveMode.Read, true);
        }

        /// <summary>
        /// 读取zip项内容
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static MemoryStream ReadEntry(ZipArchiveEntry entry)
        {
            MemoryStream memory = new MemoryStream();
            using (Stream entryStream = entry.Open())
            {
                entryStream.CopyTo(memory);
                memory.Position = 0;
            }
            return memory;
        }

        /// <summary>
        /// 读取zip项内容
        /// </summary>
        /// <param name="entryFile"></param>
        /// <returns></returns>
        public byte[] ReadContent(string entryFile)
        {
            ZipArchiveEntry entry = _archive.Entries.First(f => f.FullName == entryFile.TrimStart('/'));
            using (Stream entryStream = entry.Open())
            {
                System.Console.WriteLine(entryStream.GetType().FullName);
                using (MemoryStream memory = new MemoryStream())
                {
                    entryStream.CopyTo(memory);
                    memory.Seek(0, SeekOrigin.Begin);
                    return memory.ToArray();
                }
            }
        }

        /// <summary>
        /// 获取文档内容
        /// </summary>
        /// <returns></returns>
        public string GetBody()
        {
            ZipArchiveEntry entry = _archive.Entries.First(f => f.FullName == OfdFileName);
            if (entry == null)
            {
                return default;
            }
            MemoryStream memory = ReadEntry(entry);

            XmlDocument body = new XmlDocument();
            body.Load(memory);

            XmlNamespaceManager ns = new XmlNamespaceManager(body.NameTable);
            ns.AddNamespace("fp", "http://www.edrm.org.cn/schema/e-invoice/2019");

            XmlNode node = body.SelectSingleNode("DocBody/Signatures", ns);

            memory.Dispose();

            return node?.Value;
        }

        private static XmlDocument LoadXml(ZipArchiveEntry entry)
        {
            using (Stream entryStream = entry.Open())
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    entryStream.CopyTo(memory);
                    memory.Seek(0, SeekOrigin.Begin);
                    XmlDocument body = new XmlDocument();
                    body.Load(memory);
                    return body;
                }
            }
        }


        private static T Deserialize<T>(ZipArchiveEntry entry)
        {
            using (Stream entryStream = entry.Open())
            {
                return XmlUtils.Deserialize<T>(entryStream);
            }
        }

        /// <summary>
        /// 获取签名文件
        /// </summary>
        /// <returns></returns>
        public string GetSignatures()
        {
            ZipArchiveEntry entry = _archive.Entries.First(f => f.FullName == "OFD.xml");
            XmlDocument body = LoadXml(entry);

            XmlNode node = body.LastChild.LastChild.LastChild.LastChild;
            string signaturesFile = node.Value;

            ZipArchiveEntry signaturesBaseLoc = _archive.Entries.First(f => f.FullName == signaturesFile);
            XmlDocument signaturesBaseLocContent = LoadXml(signaturesBaseLoc);
            string signaturesBaseLocFile = signaturesBaseLocContent.LastChild.LastChild.Attributes.GetNamedItem("BaseLoc").Value;


            return signaturesBaseLocFile;


        }

        public DigestInfo GetSignature()
        {
            string signaturesBaseLocFile = GetSignatures();
            ZipArchiveEntry signatureFile = _archive.Entries.First(f => f.FullName == signaturesBaseLocFile.TrimStart('/'));
            return Deserialize<DigestInfo>(signatureFile);
        }

        public string GetSignedList()
        {
            string signatures = GetSignatures();
            ZipArchiveEntry entry = _archive.Entries.First(f => f.FullName == signatures);
            XmlDocument document = LoadXml(entry);
            XmlNode node = document.LastChild.LastChild;
            if (node == null)
            {
                return string.Empty;
            }
            ZipArchiveEntry signedEntry = _archive.Entries.First(f => f.FullName == node.Value);
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
        /// 获取发票信息
        /// </summary>
        /// <returns></returns>
        public InvoiceInfo GetInvoiceInfo()
        {
            const string invoiceEntryName = "original_invoice.xml";
            ZipArchiveEntry invoiceEntry = _archive.Entries.First(f => f.Name == invoiceEntryName);
            return Deserialize<InvoiceInfo>(invoiceEntry);
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

        /// <summary>
        /// 关闭文档
        /// </summary>
        public void Close()
        {
            _archive.Dispose();
        }
    }
}
