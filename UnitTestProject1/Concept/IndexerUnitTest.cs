using ff.Study.DesignPattern.Concept.Indexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject1.Concept
{
    [TestClass]
    public class IndexerUnitTest
    {
        [TestMethod]
        public void Test()
        {
            SingleColumnCollection c = new SingleColumnCollection();
            Assert.AreEqual<string>("china", c[0]);
            Assert.AreEqual<int>(2, c["ch"].Length); //命中china和chile两项
            Assert.AreEqual<string>("china", c["ch"][0]);
        }
    }
}
