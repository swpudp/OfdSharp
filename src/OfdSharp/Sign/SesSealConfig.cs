namespace OfdSharp.Sign
{
    public class SesSealConfig
    {
        /// <summary>
        /// 厂商
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 印章图片字节数组
        /// </summary>
        public byte[] SealPicture { get; set; }

        /// <summary>
        /// 印章图片类型
        /// </summary>
        public string SealType { get; set; }

        /// <summary>
        /// 印章名称
        /// </summary>
        public string SealName { get; set; }

        /// <summary>
        /// 印章宽度
        /// </summary>
        public int SealWidth { get; set; }

        /// <summary>
        /// 印章高度
        /// </summary>
        public int SealHeight { get; set; }

        /// <summary>
        /// 印章私钥
        /// </summary>
        public string SealPrivateKey { get; set; }

        /// <summary>
        /// 制章人证书
        /// </summary>
        public byte[] SealCert { get; set; }

        /// <summary>
        /// 签章者证书
        /// </summary>
        public byte[] SignerCert { get; set; }

        /// <summary>
        /// 签章者私钥
        /// </summary>
        public string SignerPrivateKey { get; set; }

        /// <summary>
        /// 印章标识
        /// </summary>
        public string EsId { get; set; }
    }
}
