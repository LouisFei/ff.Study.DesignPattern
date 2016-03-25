using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Creational.FactoryMethod.BatchFactory;
using ff.Study.DesignPattern.Creational.FactoryMethod.ClassicsDefine;

namespace UnitTestProject1.Creational
{
    [TestClass]
    public class FactoryMethodUnitTest3
    {
        /// <summary>
        /// 批量工厂
        /// </summary>
        [TestMethod]
        public void TestBatchFactory()
        {
            Client client = new Client();
            IProduct[] products = client.Produce();
            
            Assert.AreEqual<int>(2 + 3, products.Length);

            for (int i = 0; i < 2; i++)
            {
                Assert.AreEqual<string>("A", products[i].Name);
            }

            for (int i = 2; i < 5; i++)
            {
                Assert.AreEqual<string>("B", products[i].Name);
            }

        }

        /*
         P91
         */
    }
}
