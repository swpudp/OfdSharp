using System.Collections;
using OfdSharp.Extensions;
using OfdSharp.Ses.V1;
using Org.BouncyCastle.Asn1;


namespace OfdSharp.Ses.V4
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
        public SeSeal EsSeal { get; set; }

        /// <summary>
        /// 签章时间
        /// </summary>
        public DerGeneralizedTime TimeInfo { get; set; }

        /// <summary>
        /// 原文杂凑值
        /// </summary>
        public DerBitString DataHash { get; set; }

        /// <summary>
        /// 原文数据的属性
        /// </summary>
        public DerIA5String PropertyInfo { get; set; }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public ExtensionData ExtData { get; set; }

        public TbsSign() { }

        public TbsSign(DerInteger version, SeSeal eSeal, DerGeneralizedTime timeInfo, DerBitString dataHash, DerIA5String propertyInfo, ExtensionData extData)
        {
            Version = version;
            EsSeal = eSeal;
            TimeInfo = timeInfo;
            DataHash = dataHash;
            PropertyInfo = propertyInfo;
            ExtData = extData;
        }


        public TbsSign(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            Version = DerInteger.GetInstance(e.Next());
            EsSeal = SeSeal.GetInstance(e.Next());
            TimeInfo = DerGeneralizedTime.GetInstance(e.Next());
            DataHash = DerBitString.GetInstance(e.Next());
            PropertyInfo = DerIA5String.GetInstance(e.Next());
            if (e.MoveNext())
            {
                ExtData = ExtensionData.GetInstance(e.Current);
            }
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
            Asn1EncodableVector v = new Asn1EncodableVector(6)
            {
                Version,
                EsSeal,
                TimeInfo,
                DataHash,
                PropertyInfo
            };
            if (ExtData != null)
            {
                v.Add(ExtData);
            }
            return new DerSequence(v);
        }
    }
}
