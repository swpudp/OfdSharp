using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfdSharp.Primitives.Tests
{
    [TestClass()]
    public class ArrayTests
    {
        [TestMethod()]
        public void ArrayTest()
        {
            Array a1 = new Array("1", "2", "3", "4", "5", "6");
            Assert.IsNotNull(a1);
        }

        [TestMethod()]
        public void FormatTest()
        {
            Array a1 = new Array("1", "2", "3", "4", "5", "6");
            Assert.IsNotNull(a1);
            Assert.AreEqual("1 2 3 4 5 6", a1.ToString());
        }

        [TestMethod()]
        public void ToMatrixTest()
        {
            Array a1 = new Array("1", "2", "3", "4", "5", "6");
            Assert.IsNotNull(a1);
            double[,] matrix = a1.ToMatrix();
            Assert.AreEqual(9, matrix.Length);
        }

        [TestMethod()]
        public void MultiplyTest()
        {
            Array a1 = new Array("1", "2", "3", "4", "5", "6");
            Array a2 = new Array("10", "20", "30", "40", "50", "60");
            Array a3 = a1 * a2;
            Assert.AreEqual(9, a3.ToMatrix().Length);
        }
    }
}