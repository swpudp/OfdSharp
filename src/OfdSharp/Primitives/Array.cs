﻿using System;
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
        private readonly List<string> _array = new List<string>();

        /// <summary>
        /// 获取一个单位矩阵变换参数
        /// </summary>
        /// <returns>单位CTM举证</returns>
        public static Array UnitCTM()
        {
            return new Array(
                "1", "0", // 0
                "0", "1", // 0
                "0", "0"  // 1
            );
        }

        public Array MtxMul(Array array)
        {
            if (this._array.Count != 6 || array._array.Count != 6)
            {
                throw new ArgumentOutOfRangeException();
            }
            double[,] a = ToMtx();
            double[,] b = array.ToMtx();

            double[,] res = new double[3, 3];

            for (int k = 0; k < 3; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        res[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return new Array(Format(res[0, 0]), Format(res[0, 1]), Format(res[1, 0]), Format(res[1, 1]), Format(res[2, 0]), Format(res[2, 1]));
        }


        public Array(params string[] arr)
        {
            _array = new List<string>(arr.Length);
            foreach (string s in arr)
            {
                _array.Add(s);
            }
        }

        public static string Format(double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public int Size => _array.Count;

        public double[,] ToMtx()
        {
            if (Size != 6)
            {
                throw new ArgumentOutOfRangeException("矩阵数组必须有 9个元素");
            }
            double[,] mtx = new double[3, 3];
            mtx[0, 0] = double.Parse(_array.ElementAt(0));
            mtx[0, 1] = double.Parse(_array.ElementAt(1));
            mtx[0, 2] = 0;

            mtx[1, 0] = double.Parse(_array.ElementAt(2));
            mtx[1, 1] = double.Parse(_array.ElementAt(3));
            mtx[1, 2] = 0;

            mtx[2, 0] = double.Parse(_array.ElementAt(4));
            mtx[2, 1] = double.Parse(_array.ElementAt(5));
            mtx[2, 2] = 1;

            return mtx;
        }
    }
}
