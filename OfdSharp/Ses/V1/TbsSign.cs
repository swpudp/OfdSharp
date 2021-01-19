using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;
using System.Collections;

namespace OfdSharp.Ses.V1
{
    /// <summary>
    /// 签章信息
    /// </summary>
    public class TbsSign : Asn1Encodable
    {
        /// <summary>
        /// 电子印章版本号，与电子印章版本号保持一致
        /// </summary>
        public DerInteger Version { get; set; }

        /// <summary>
        /// 电子印章
        /// </summary>
        public SesSealInfo EsSeal { get; set; }

        /// <summary>
        /// 签章时间
        /// </summary>
        public DerBitString TimeInfo { get; set; }

        /// <summary>
        /// 原文杂凑值
        /// </summary>
        public DerBitString DataHash { get; set; }

        /// <summary>
        /// 原文数据的属性
        /// </summary>
        public DerIA5String PropertyInfo { get; set; }

        /// <summary>
        /// 签章人对应的签名证书
        /// </summary>
        public Asn1OctetString Cert { get; set; }

        /// <summary>
        /// 签名算法标识符
        /// </summary>
        public DerObjectIdentifier SignatureAlgorithm { get; set; }

        public TbsSign() { }

        public TbsSign(DerInteger version, SesSealInfo eSeal, DerBitString timeInfo, DerBitString dataHash, DerIA5String propertyInfo, DerObjectIdentifier signatureAlgorithm)
        {
            Version = version;
            EsSeal = eSeal;
            TimeInfo = timeInfo;
            DataHash = dataHash;
            PropertyInfo = propertyInfo;
            SignatureAlgorithm = signatureAlgorithm;
        }


        public TbsSign(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            Version = DerInteger.GetInstance(e.Next());
            EsSeal = SesSealInfo.GetInstance(e.Next());
            TimeInfo = DerBitString.GetInstance(e.Next());
            DataHash = DerBitString.GetInstance(e.Next());
            PropertyInfo = DerIA5String.GetInstance(e.Next());
            Cert = Asn1OctetString.GetInstance(e.Next());
            SignatureAlgorithm = DerObjectIdentifier.GetInstance(e.Next());
        }

        public static TbsSign GetInstance(object o)
        {
            if (o is TbsSign sign)
            {
                return sign;
            }
            return o != null ? new TbsSign(Asn1Sequence.GetInstance(o)) : null;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(7)
            {
                Version,
                EsSeal,
                TimeInfo,
                DataHash,
                PropertyInfo,
                Cert,
                SignatureAlgorithm
            };
            return new DerSequence(v);
        }
    }
}
