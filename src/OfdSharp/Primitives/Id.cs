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
    public struct Id : IFormattable, IComparable
    {
        private readonly int _value;
        private static int _newValue;

        public Id(int value)
        {
            _value = value;
        }

        public static Id NewId()
        {
            Interlocked.Increment(ref _newValue);
            return new Id(_newValue);
        }

        public int CompareTo(object obj)
        {
            return Convert.ToInt32(obj);
        }

        public override string ToString() => ToString("D", null);

        public string ToString(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            Console.WriteLine("format is " + format + ",formatProvider is " + formatProvider);
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Id id && _value == id._value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Id a, Id b) => a._value == b._value;

        public static bool operator !=(Id a, Id b) => a._value != b._value;
    }
}
