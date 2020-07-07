using System;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.Parse
{
    /// <summary>
    /// 电子签章版本解析器
    /// </summary>
    public static class VersionParser
    {
        /// <summary>
        /// 解析版本持有者
        /// </summary>
        /// <param name="o">带解析数据，可以是字节串也可以是ASN1对象</param>
        /// <returns>带有版本的ASN1对象序列</returns>
        public static SesVersionHolder ParseSealVersion(object o)
        {
            Asn1Sequence sequence = Asn1Sequence.GetInstance(o);
            if (sequence.Count == 4)
            {
                return new SesVersionHolder(SesVersion.V4, sequence);
            }
            if (sequence.Count == 2)
            {
                return new SesVersionHolder(SesVersion.V1, sequence);
            }
            throw new NotSupportedException("not supported SesVersion");
        }

        /// <summary>
        /// 解析电子签章数据版本
        /// </summary>
        /// <param name="o">带解析数据，可以是字节串也可以是ASN1对象</param>
        /// <returns>带有版本的ASN1对象序列</returns>
        public static SesVersionHolder ParseSignatureVersion(object o)
        {
            Asn1Sequence sequence = Asn1Sequence.GetInstance(o);
            if (sequence.Count >= 4 && sequence.Count <= 5)
            {
                return new SesVersionHolder(SesVersion.V4, sequence);
            }
            if (sequence.Count == 2)
            {
                return new SesVersionHolder(SesVersion.V1, sequence);
            }
            throw new NotSupportedException("not supported SesVersion");
        }

    }
}
