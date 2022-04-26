using System;
using System.ComponentModel;
using System.Threading;

namespace OfdSharp.Primitives
{
    /// <summary>
    /// 标识，无符号整数，应在文档内唯一。0表示无效标识
    /// </summary>
    [Serializable]
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public struct CtId : IFormattable, IComparable
    {
        private readonly int _value;

        public CtId(int value)
        {
            _value = value;
        }

        public CtId(string value)
        {
            _value = int.Parse(value);
        }

        public int CompareTo(object obj)
        {
            return Convert.ToInt32(obj);
        }

        public override string ToString() => ToString("D", null);

        public string ToString(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is CtId id && _value == id._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(CtId a, CtId b) => a._value == b._value;

        public static bool operator !=(CtId a, CtId b) => a._value != b._value;
    }
}