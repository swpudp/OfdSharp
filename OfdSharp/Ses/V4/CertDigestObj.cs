using System;
using System.Collections;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V4
{
    /// <summary>
    /// 签章者证书杂凑值
    /// </summary>
    public class CertDigestObj : Asn1Encodable
    {
        /// <summary>
        /// 自定义类型
        /// </summary>
        public DerPrintableString Type { get; }

        /// <summary>
        /// 证书杂凑值
        /// </summary>
        public Asn1OctetString Value { get; private set; }

        public CertDigestObj(DerPrintableString type, Asn1OctetString value)
        {
            Type = type;
            Value = value;
        }

        public CertDigestObj(Asn1Sequence sequence)
        {
            IEnumerator enumerator = sequence.GetEnumerator();

            enumerator.MoveNext();
            Type = DerPrintableString.GetInstance(enumerator.Current);

            enumerator.MoveNext();
            Value = Asn1OctetString.GetInstance(enumerator.Current);
        }

        public void SetValue(byte[] value)
        {
            Value = new DerOctetString(value);
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector vector = new Asn1EncodableVector(2) { Type, Value };
            return new DerSequence(vector);
        }

        /// <summary>
        /// 获取签章者证书杂凑值对象实例
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static CertDigestObj GetInstance(Object o)
        {
            if (o is CertDigestObj obj)
            {
                return obj;
            }
            return o != null ? new CertDigestObj(Asn1Sequence.GetInstance(o)) : null;
        }
    }
}
