using System;

namespace OfdSharp.Primitives
{
    /// <summary>
    /// 标识，无符号整数，应在文档内唯一。0表示无效标识
    /// </summary>
    [Serializable]
    public struct Id : IFormattable, IComparable
    {


        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}
