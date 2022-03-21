namespace OfdSharp.Crypto
{
    /// <summary>
    /// 密钥对
    /// </summary>
    public class CipherKeyPair
    {
        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey { get; }

        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; }

        public CipherKeyPair(string publicKey, string privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }
    }
}
