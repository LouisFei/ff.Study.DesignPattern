using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Creational.Singleton.Classics;

namespace UnitTestProject1.Creational
{
    [TestClass]
    public class SingletonUnitTest
    {
        [TestMethod]
        public void TestCounter()
        {
            Counter.Instance.Reset();
            Assert.AreEqual<int>(1, Counter.Instance.Next);
            Assert.AreEqual<int>(2, Counter.Instance.Next);
            Assert.AreEqual<int>(3, Counter.Instance.Next);
        }
    }
}
