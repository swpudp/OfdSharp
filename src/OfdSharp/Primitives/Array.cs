using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OfdSharp.Primitives
{
    /// <summary>
    /// 数组，以空格来分割元素。元素可以是除ST_Loc、ST_Array外的数据类型，不可嵌套
    /// 实例："1 2.0 5.0"
    /// </summary>
    public class Array
    {
        /// <summary>
        /// 元素收容
        /// </summary>
        private readonly List<string> _elements;

        /// <summary>
        /// 获取一个单位矩阵变换参数 0 0 1
        /// </summary>
        /// <returns>单位CTM举证</returns>
        public static Array UnitMatrix => new Array("1", "0", "0", "1", "0", "0");

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="arr"></param>
        public Array(params string[] arr)
        {
            _elements = new List<string>(arr.Length);
            _elements.AddRange(arr);
        }

        /// <summary>
        /// 从字符串解析
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Array Parse(string value)
        {
            string[] values = value.Split(' ');
            return new Array(values);
        }

        /// <summary>
        /// 矩阵相乘
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static Array operator *(Array a1, Array a2)
        {
            if (a1.Size != 6 || a2.Size != 6)
            {
                throw new ArgumentOutOfRangeException();
            }
            double[,] a = a1.ToMatrix();
            double[,] b = a2.ToMatrix();
            double[,] result = new double[3, 3];
            for (int k = 0; k < 3; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return new Array(Format(result[0, 0]), Format(result[0, 1]), Format(result[1, 0]), Format(result[1, 1]), Format(result[2, 0]), Format(result[2, 1]));
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string Format(double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 元素个数
        /// </summary>
        public int Size => _elements.Count;

        /// <summary>
        /// 转换成矩阵
        /// </summary>
        /// <returns></returns>
        public double[,] ToMatrix()
        {
            if (Size != 6)
            {
                throw new ArgumentOutOfRangeException("矩阵数组必须有 9个元素");
            }
            double[,] matrix = new double[3, 3];
            matrix[0, 0] = double.Parse(_elements.ElementAt(0));
            matrix[0, 1] = double.Parse(_elements.ElementAt(1));
            matrix[0, 2] = 0;
            matrix[1, 0] = double.Parse(_elements.ElementAt(2));
            matrix[1, 1] = double.Parse(_elements.ElementAt(3));
            matrix[1, 2] = 0;
            matrix[2, 0] = double.Parse(_elements.ElementAt(4));
            matrix[2, 1] = double.Parse(_elements.ElementAt(5));
            matrix[2, 2] = 1;
            return matrix;
        }

        public override string ToString()
        {
            return string.Join(" ", _elements);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Array a))
            {
                return false;
            }
            return a._elements.SequenceEqual(_elements);
        }

        public static bool operator ==(Array a1, Array a2)
        {
            return !(ReferenceEquals(a1, null) || ReferenceEquals(a2, null)) && a1._elements.SequenceEqual(a2._elements);
        }

        public static bool operator !=(Array a1, Array a2)
        {
            //都是null
            if (ReferenceEquals(a1, null) && ReferenceEquals(a2, null))
            {
                return false;
            }
            //其中一个null
            if (ReferenceEquals(a1, null) || ReferenceEquals(a2, null))
            {
                return true;
            }
            return !(a1 == a2);
        }

        public override int GetHashCode()
        {
            return string.Join(" ", _elements).GetHashCode();
        }
    }
}
