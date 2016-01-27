using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Creational.FactoryMethod.Example1;

namespace UnitTestProject1.Creational
{
    [TestClass]
    public class FactoryMethodUnitTest
    {
        [TestMethod]
        public void Example1Test()
        {
            Factory factory = new Factory();
            IProduct product = factory.Create();
            Assert.AreEqual<Type>(product.GetType(), typeof(ConcreteProductA));
        }

        /// <summary>
        /// 说明静态工厂可以根据提供的目标类型枚举变量选择需要实例化的类型
        /// </summary>
        [TestMethod]
        public void StaticFactoryTest()
        {
            IProduct product = ProductFactory.Create(Category.B);
            Assert.IsNotNull(product);
            Assert.AreEqual<Type>(typeof(ConcreteProductB), product.GetType());
        }
    }
}
