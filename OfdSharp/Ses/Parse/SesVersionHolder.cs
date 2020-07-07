using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.Parse
{
    /// <summary>
    /// 电子签章版本持有者
    /// </summary>
    public class SesVersionHolder
    {
        /// <summary>
        /// 电子签章版本号
        /// </summary>
        public SesVersion Version { get; }

        /// <summary>
        /// Asn1序列对象
        /// </summary>
        public Asn1Sequence Sequence { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="version"></param>
        /// <param name="sequence"></param>
        public SesVersionHolder(SesVersion version, Asn1Sequence sequence)
        {
            Version = version;
            Sequence = sequence;
        }
    }
}
