using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Creational.FactoryMethod.ClassicsDefine;
using ff.Study.DesignPattern.Creational.FactoryMethod.ClassicsDefine.ConfigBased;

namespace UnitTestProject1.Creational
{
    [TestClass]
    public class FactoryMethodUnitTest2
    {
        [TestMethod]
        public void FactoryMethodTest()
        {
            IFactory factory = (new Assembler()).Create<IFactory>();
            Client client = new Client(factory); //注入IFactory
            IProduct product = client.GetProduct(); //通过IFactory获取IProduct
            Assert.AreEqual<string>("A", product.Name); //配置中选择为FactoryA
        }
    }
}
