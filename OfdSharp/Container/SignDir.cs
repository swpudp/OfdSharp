using System.IO;
using System.Xml;
using OfdSharp.Core.Signs;

namespace OfdSharp.Container
{
    public class SignDir : VirtualContainer
    {
        /// <summary>
        /// 签名容器名称前缀
        /// </summary>
        public const string SignContainerPrefix = "Sign_";

        /// <summary>
        /// 电子印章文件名
        /// </summary>
        private const string SealFileName = "Seal.esl";

        /// <summary>
        /// 签名/签章 描述文件名
        /// </summary>
        private const string SignatureFileName = "Signature.xml";

        /// <summary>
        /// 签名值文件名
        /// </summary>
        private const string SignedValueFileName = "SignedValue.dat";

        public SignDir(DirectoryInfo fullDir) : base(fullDir)
        {
        }

        /// <summary>
        /// 签名/签章 描述文件
        /// </summary>
        /// <returns></returns>
        public SignDesc GetSignature()
        {
            XmlDocument ele = GetDocument(SignatureFileName);
            return new SignDesc(ele);
        }

        /// <summary>
        /// 电子印章文件
        /// </summary>
        /// <returns></returns>
        public FileInfo GetSeal()
        {
            return new FileInfo(SealFileName);
        }

        /// <summary>
        /// 签名值文件
        /// </summary>
        /// <returns></returns>
        public FileInfo GetSignedValue()
        {
            return new FileInfo(SignedValueFileName);
        }
    }
}
