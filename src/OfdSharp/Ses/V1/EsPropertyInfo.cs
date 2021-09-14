using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;
using System.Collections;

namespace OfdSharp.Ses.V1
{
    /// <summary>
    /// 印章属性信息
    /// </summary>
    public class EsPropertyInfo : Asn1Encodable
    {
        /// <summary>
        /// 单位印章类型
        /// </summary>

        public static DerInteger OrgType = new DerInteger(1);

        /// <summary>
        /// 个人印章类型
        /// </summary>
        public static DerInteger PersonType = new DerInteger(1);

        /// <summary>
        /// 印章类型
        /// </summary>
        public DerInteger Type { get; }

        /// <summary>
        /// 印章名称
        /// </summary>
        public DerUtf8String Name { get; }

        /// <summary>
        /// 签章人证书列表
        /// </summary>
        public Asn1Sequence CertList { get; }

        /// <summary>
        /// 印章制做日期
        /// </summary>
        public DerUtcTime CreateDate { get; }

        /// <summary>
        /// 印章有效起始日期
        /// </summary>
        public DerUtcTime ValidStart { get; }

        /// <summary>
        /// 印章有效终止日期
        /// </summary>
        public DerUtcTime ValidEnd { get; }

        public EsPropertyInfo() { }

        public EsPropertyInfo(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            Type = DerInteger.GetInstance(e.Next());
            Name = DerUtf8String.GetInstance(e.Next());
            CertList = Asn1Sequence.GetInstance(e.Next());
            CreateDate = DerUtcTime.GetInstance(e.Next());
            ValidStart = DerUtcTime.GetInstance(e.Next());
            ValidEnd = DerUtcTime.GetInstance(e.Next());
        }

        public EsPropertyInfo(DerInteger type, DerUtf8String name, Asn1Sequence certList, DerUtcTime createDate, DerUtcTime validStart, DerUtcTime validEnd)
        {
            Type = type;
            Name = name;
            CertList = certList;
            CreateDate = createDate;
            ValidStart = validStart;
            ValidEnd = validEnd;
        }

        public static EsPropertyInfo GetInstance(object o)
        {
            if (o is EsPropertyInfo esPropertyInfo)
            {
                return esPropertyInfo;
            }
            return o != null ? new EsPropertyInfo(Asn1Sequence.GetInstance(o)) : null;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(6)
            {
                Type,
                Name,
                CertList,
                CreateDate,
                ValidStart,
                ValidEnd
            };
            return new DerSequence(v);
        }
    }
}
