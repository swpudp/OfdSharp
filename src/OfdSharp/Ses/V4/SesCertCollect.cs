using System;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V4
{
    /// <summary>
    /// 签章者证书信息列表
    /// </summary>
    public class SesCertCollect : Asn1Encodable, IAsn1Choice
    {
        /// <summary>
        /// 签章者证书
        /// </summary>
        private readonly CertInfoCollect _certInfoCollect;

        /// <summary>
        /// 签章者证书杂凑值
        /// </summary>
        private readonly CertDigestCollect _certDigestCollect;

        public SesCertCollect(CertInfoCollect certInfoCollect)
        {
            _certInfoCollect = certInfoCollect;
        }

        public SesCertCollect(CertDigestCollect certDigestCollect)
        {
            _certDigestCollect = certDigestCollect;
        }

        public static SesCertCollect GetInstance(DerInteger type, object o)
        {
            if (o is SesCertCollect sesCertCollect)
            {
                return sesCertCollect;
            }
            if (o == null)
            {
                return null;
            }
            if (o is Asn1Encodable)
            {
                int value = type.Value.IntValue;
                if (value == 1)
                {
                    return new SesCertCollect(CertInfoCollect.GetInstance(o));
                }
                if (value == 2)
                {
                    return new SesCertCollect(CertDigestCollect.GetInstance(o));
                }
                throw new NotSupportedException($"unknown type in getInstance():{o.GetType().Name}");
            }
            if (o is byte[] bytes)
            {
                try
                {
                    return GetInstance(type, Asn1Object.FromByteArray(bytes));
                }
                catch (Exception e)
                {
                    throw new NotSupportedException("unknown encoding in getInstance()", e);
                }
            }
            throw new NotSupportedException($"unknown object in getInstance():{o.GetType().Name}");
        }

        public Asn1Encodable Get()
        {
            if (_certInfoCollect != null)
            {
                return _certInfoCollect;
            }
            return _certDigestCollect;
        }

        public override Asn1Object ToAsn1Object()
        {
            return Get().ToAsn1Object();
        }
    }
}
