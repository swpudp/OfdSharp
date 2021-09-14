using System.Collections;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V1
{
    public class SesSignInfo : Asn1Encodable
    {
        /// <summary>
        /// 代表对电子印章数据进行签名的制章人证书
        /// </summary>
        public Asn1OctetString Cert { get; }

        /// <summary>
        /// 代表签名算法OID标识
        /// 遵循 GM/T 006
        /// </summary>
        public DerObjectIdentifier SignatureAlgorithm { get; }

        /// <summary>
        /// 制章人的签名值
        /// 制章人对电子印章格式中印章信息SES_SealInfo、制章人证书、签名算法标识符按 SEQUENCE方式组成的信息内容的数字签名
        /// </summary>
        public DerBitString SignData { get; }

        public static SesSignInfo GetInstance(object o)
        {
            if (o is SesSignInfo)
            {
                return (SesSignInfo)o;
            }
            else if (o != null)
            {
                return new SesSignInfo(Asn1Sequence.GetInstance(o));
            }
            return null;
        }

        public SesSignInfo(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            Cert = Asn1OctetString.GetInstance(e.Next());
            SignatureAlgorithm = DerObjectIdentifier.GetInstance(e.Next());
            SignData = DerBitString.GetInstance(e.Next());
        }

        public SesSignInfo(Asn1OctetString cert, DerObjectIdentifier signatureAlgorithm, DerBitString signData)
        {
            Cert = cert;
            SignatureAlgorithm = signatureAlgorithm;
            SignData = signData;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(3) { Cert, SignatureAlgorithm, SignData };
            return new DerSequence(v);
        }
    }
}
