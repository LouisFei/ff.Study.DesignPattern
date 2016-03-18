using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Creational.FactoryMethod.SimpleFactory;

namespace UnitTestProject1.Creational
{
    [TestClass]
    public class FactoryMethodUnitTest
    {
        /// <summary>
        /// 可以按照要求生成抽象类型，但具体实例化哪个类型由工厂决定
        /// </summary>
        [TestMethod]
        public void Example1Test()
        {
            SimpleFactory factory = new SimpleFactory();
            IProduct product = factory.Create();
            Assert.AreEqual<Type>(product.GetType(), typeof(ConcreteProductA));
        }

        /// <summary>
        /// 说明静态工厂可以根据提供的目标类型枚举变量选择需要实例化的类型
        /// </summary>
        [TestMethod]
        public void StaticFactoryTest()
        {
            IProduct product = ParametricProductFactory.Create(Category.B);
            Assert.IsNotNull(product);
            Assert.AreEqual<Type>(typeof(ConcreteProductB), product.GetType());
        }

        
    }
}
