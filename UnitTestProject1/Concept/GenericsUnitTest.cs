using ff.Study.DesignPattern.Concept.Generics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject1.Concept
{
    [TestClass]
    public class GenericsUnitTest
    {
        interface IProduct { }
        class ConcreteProduct : IProduct { }

        [TestMethod]
        public void RawGenericFactoryTest()
        {
            string typeName = typeof(ConcreteProduct).AssemblyQualifiedName;
            IProduct product = RawGenericFactory.Create<IProduct>(typeName);

            Assert.IsNotNull(product);
            Assert.AreEqual<string>(typeName, product.GetType().AssemblyQualifiedName);
        }
    }
}
