using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Primitives;

namespace UnitTests.Primitives
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        public void ArrayTest()
        {
            CtArray a1 = new CtArray("1", "2", "3", "4", "5", "6");
            Assert.IsNotNull(a1);
        }

        [TestMethod]
        public void FormatTest()
        {
            CtArray a1 = new CtArray("1", "2", "3", "4", "5", "6");
            Assert.IsNotNull(a1);
            Assert.AreEqual("1 2 3 4 5 6", a1.ToString());
        }

        [TestMethod]
        public void ToMatrixTest()
        {
            CtArray a1 = new CtArray("1", "2", "3", "4", "5", "6");
            Assert.IsNotNull(a1);
            double[,] matrix = a1.ToMatrix();
            Assert.AreEqual(9, matrix.Length);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            CtArray a1 = new CtArray("1", "2", "3", "4", "5", "6");
            CtArray a2 = new CtArray("10", "20", "30", "40", "50", "60");
            CtArray a3 = a1 * a2;
            Assert.AreEqual(9, a3.ToMatrix().Length);
        }
    }
}