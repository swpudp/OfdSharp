using OfdSharp.Reader;
using System.Collections.Generic;
using OfdSharp.Core.Signature;

namespace OfdSharp.Sign.Stamp
{
    /// <summary>
    /// 签章外观位置提供者
    /// </summary>
    public interface IStampAppearance
    {
        /// <summary>
        /// 获取签章外观
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="idProvider"></param>
        /// <returns></returns>
        List<StampAnnot> GetAppearance(OfdReader ctx, AtomicSignId idProvider);
    }
}
