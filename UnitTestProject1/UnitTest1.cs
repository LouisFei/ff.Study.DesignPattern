using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int num1 = 100;
            int num2 = 200;

            var result = new ff.Study.DesignPattern.Test().Add(num1, num2);

            Assert.AreEqual(300, result);
        }
    }
}
