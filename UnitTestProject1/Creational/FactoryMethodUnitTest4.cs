using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Creational.FactoryMethod.GenericFactory;
using ff.Study.DesignPattern.Creational.FactoryMethod.DelegateFactory;

namespace UnitTestProject1.Creational
{
    [TestClass]
    public class FactoryMethodUnitTest4
    {
        /// <summary>
        /// 委托工厂
        /// </summary>
        [TestMethod]
        public void TestDelegateFactory()
        {
            IFactory<CalculateHandler> factory = new CalculateHandlerFactory();
            CalculateHandler handler = factory.Create();

            Assert.AreEqual<int>(1 + 2 + 3, handler(1, 2, 3));
        }

        /*
         P94
         */
    }
}
