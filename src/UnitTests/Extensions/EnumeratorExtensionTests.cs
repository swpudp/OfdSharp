using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfdSharp.Extensions;
using System.Collections.Generic;

namespace UnitTests.Extensions
{
    /// <summary>
    /// 迭代器扩展测试
    /// </summary>
    [TestClass]
    public class EnumeratorExtensionTests
    {
        /// <summary>
        /// 取下一个元素测试
        /// </summary>
        [TestMethod]
        public void NextTest()
        {
            IList<int> testList = new List<int> { 2, 3, 4, 5 };
            using (IEnumerator<int> e = testList.GetEnumerator())
            {
                Assert.AreEqual(2, e.Next());
                Assert.AreEqual(3, e.Next());
                Assert.AreEqual(4, e.Next());
                Assert.AreEqual(5, e.Next());
            }
        }
    }
}