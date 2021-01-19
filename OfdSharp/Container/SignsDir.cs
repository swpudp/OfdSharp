using System;
using System.IO;
using System.Xml;
using OfdSharp.Core.Signs;

namespace OfdSharp.Container
{
    /// <summary>
    /// 签名容器
    /// </summary>
    public class SignsDir : VirtualContainer
    {
        /// <summary>
        /// 签名Index最大值 + 1
        /// 也就是签名容器数量，因为签名容器Index从0起算
        /// </summary>
        private int _maxSignIndex;

        /// <summary>
        /// 签名列表文件名称
        /// </summary>
        private const string SignaturesFileName = "Signatures.xml";

        private void InitContainer()
        {
            //FileInfo fullDirFile = new FileInfo(SignaturesFileName);
            //File[] files = fullDirFile.listFiles();
            //if (files != null)
            //{
            //    // 遍历容器中已经有的签名目录，初始签名数量
            //    for (File f : files)
            //    {
            //        String dirName = f.getName();
            //        // 签名目录名为： Sign_N
            //        if (dirName.startsWith(SignDir.SignContainerPrefix))
            //        {
            //            String numb = dirName.replace(SignDir.SignContainerPrefix, "");
            //            int num = Integer.parseInt(numb);
            //            if (maxSignIndex <= num)
            //            {
            //                maxSignIndex = num + 1;
            //            }
            //        }
            //    }
            //}
        }

        public SignsDir(DirectoryInfo fullDir) : base(fullDir)
        {
            InitContainer();
        }

        /// <summary>
        /// 签名列表文件
        /// </summary>
        /// <returns></returns>
        public SignatureCollect GetSignatures()
        {
            XmlDocument element = GetDocument(SignaturesFileName);
            return new SignatureCollect();
        }

        /// <summary>
        /// 创建一个签名/章虚拟容器
        /// 签名/章虚拟容器
        /// </summary>
        /// <returns></returns>
        public SignDir NewSignDir()
        {
            // 新的签名容器一定是最大Index，并且此时目录中并不存在该目录
            string name = SignDir.SignContainerPrefix + _maxSignIndex;
            _maxSignIndex++;
            // 创建容器
            return new SignDir(new DirectoryInfo(name));
        }

        /// <summary>
        /// 获取指定签名容器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SignDir GetByIndex(int index)
        {
            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            string containerName = SignDir.SignContainerPrefix + index;
            return new SignDir(new DirectoryInfo(containerName));
        }

        /// <summary>
        /// 获取指定签名容器
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public SignDir GetSignDir(string containerName) 
        {
            return new SignDir(new DirectoryInfo(containerName));
    }
}
}
