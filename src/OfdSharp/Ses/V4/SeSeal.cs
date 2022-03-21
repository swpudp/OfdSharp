using System.Collections;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V4
{
    /// <summary>
    /// 电子印章数据
    /// </summary>
    public class SeSeal : Asn1Encodable
    {
        /// <summary>
        /// 印章信息
        /// </summary>
        public SealInfo SealInfo { get; set; }

        /// <summary>
        /// 制章人证书
        /// </summary>
        public Asn1OctetString Cert { get; set; }

        /// <summary>
        /// 签名算法标识符
        /// </summary>
        public DerObjectIdentifier SignAlgId { get; set; }

        /// <summary>
        /// 签名值，制章者对电子印章格式中的印章信息域SES_SealInfo，按照Sequence方式组成的信息内容进行数字签名所得的结果
        /// </summary>
        public DerBitString SignedValue { get; set; }

        public SeSeal() { }

        public SeSeal(SealInfo eSealInfo, Asn1OctetString cert, DerObjectIdentifier signAlgId, DerBitString signedValue)
        {
            SealInfo = eSealInfo;
            Cert = cert;
            SignAlgId = signAlgId;
            SignedValue = signedValue;
        }

        public SeSeal(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            SealInfo = SealInfo.GetInstance(e.Next());
            Cert = Asn1OctetString.GetInstance(e.Next());
            SignAlgId = DerObjectIdentifier.GetInstance(e.Next());
            SignedValue = DerBitString.GetInstance(e.Next());
        }

        public static SeSeal GetInstance(object o)
        {
            if (o is SeSeal seSeal)
            {
                return seSeal;
            }
            return o != null ? new SeSeal(Asn1Sequence.GetInstance(o)) : null;
        }

        public SeSeal SetCert(byte[] cert)
        {
            Cert = new DerOctetString(cert);
            return this;
        }

        public SeSeal SetSignedValue(byte[] signedValue)
        {
            SignedValue = new DerBitString(signedValue);
            return this;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(4) { SealInfo, Cert, SignAlgId, SignedValue };
            return new DerSequence(v);
        }
    }
}
