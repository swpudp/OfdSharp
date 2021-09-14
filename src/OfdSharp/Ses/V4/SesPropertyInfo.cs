using System.Collections;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V4
{
    /// <summary>
    /// 印章属性
    /// </summary>
    public class SesPropertyInfo : Asn1Encodable
    {
        /// <summary>
        /// 签章者证书信息类型： 1 - 数字证书类型
        /// </summary>
        public static readonly DerInteger CertType = new DerInteger(1);

        /// <summary>
        /// 签章者证书信息类型： 2 - 数字证书杂凑值
        /// </summary>
        public static readonly DerInteger CertDigestType = new DerInteger(2);

        /// <summary>
        /// 印章类型
        /// </summary>
        public DerInteger Type { get; }

        /// <summary>
        /// 印章名称
        /// </summary>
        public DerUtf8String Name { get; }

        /// <summary>
        /// 签章者证书信息类型
        /// </summary>
        public DerInteger CertListType { get; }

        /// <summary>
        /// 签章者证书信息列表
        /// </summary>
        public SesCertCollect CertList { get; }

        /// <summary>
        /// 印章制做日期
        /// </summary>
        public DerGeneralizedTime CreateDate { get; }

        /// <summary>
        /// 印章有效起始日期
        /// </summary>
        public DerGeneralizedTime ValidStart { get; }

        /// <summary>
        /// 印章有效终止日期
        /// </summary>
        public DerGeneralizedTime ValidEnd { get; }


        public SesPropertyInfo(DerInteger type,
            DerUtf8String name,
            DerInteger certListType,
            SesCertCollect certList,
            DerGeneralizedTime createDate,
            DerGeneralizedTime validStart,
            DerGeneralizedTime validEnd)
        {
            Type = type;
            Name = name;
            CertList = certList;
            CertListType = certListType;
            CreateDate = createDate;
            ValidEnd = validEnd;
            ValidStart = validStart;
        }

        public SesPropertyInfo(Asn1Sequence sequence)
        {
            IEnumerator e = sequence.GetEnumerator();
            Type = DerInteger.GetInstance(e.Next());
            Name = DerUtf8String.GetInstance(e.Next());
            CertListType = DerInteger.GetInstance(e.Next());
            CertList = SesCertCollect.GetInstance(CertListType, e.Next());
            CreateDate = DerGeneralizedTime.GetInstance(e.Next());
            ValidStart = DerGeneralizedTime.GetInstance(e.Next());
            ValidEnd = DerGeneralizedTime.GetInstance(e.Next());
        }

        public static SesPropertyInfo GetInstance(object o)
        {
            if (o is SesPropertyInfo sesPropertyInfo)
            {
                return sesPropertyInfo;
            }
            return o != null ? new SesPropertyInfo(Asn1Sequence.GetInstance(o)) : null;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector vector = new Asn1EncodableVector(7)
            {
                Type,
                Name,
                CertListType,
                CertList,
                CreateDate,
                ValidStart,
                ValidEnd
            };
            return new DerSequence(vector);
        }
    }
}
