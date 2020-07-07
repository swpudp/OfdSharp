using System.Collections;
using OfdSharp.Extensions;
using Org.BouncyCastle.Asn1;

namespace OfdSharp.Ses.V1
{
    /// <summary>
    /// 印章图片信息
    /// </summary>
    public class SesPictureInfo : Asn1Encodable
    {
        /// <summary>
        /// 图片类型
        /// 代表印章图片类型，如 GIF、BMP、JPG、SVG等
        /// </summary>
        public DerIA5String Type { get; }

        /// <summary>
        /// 印章图片数据
        /// </summary>
        public Asn1OctetString Data { get; }

        /// <summary>
        /// 图片显示宽度，单位为毫米（mm）
        /// </summary>
        public DerInteger Width { get; }

        /// <summary>
        /// 图片显示高度，单位为毫米（mm）
        /// </summary>
        public DerInteger Height { get; }

        public SesPictureInfo(Asn1Sequence seq)
        {
            IEnumerator e = seq.GetEnumerator();
            Type = DerIA5String.GetInstance(e.Next());
            Data = Asn1OctetString.GetInstance(e.Next());
            Width = DerInteger.GetInstance(e.Next());
            Height = DerInteger.GetInstance(e.Next());
        }

        public SesPictureInfo(DerIA5String type, Asn1OctetString data, DerInteger width, DerInteger height)
        {
            Type = type;
            Data = data;
            Width = width;
            Height = height;
        }

        public static SesPictureInfo GetInstance(object o)
        {
            if (o is SesPictureInfo sesPictureInfo)
            {
                return sesPictureInfo;
            }
            return o != null ? new SesPictureInfo(Asn1Sequence.GetInstance(o)) : null;
        }

        public override Asn1Object ToAsn1Object()
        {
            Asn1EncodableVector v = new Asn1EncodableVector(4) { Type, Data, Width, Height };
            return new DerSequence(v);
        }
    }
}
