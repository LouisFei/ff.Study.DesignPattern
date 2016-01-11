using ff.Study.DesignPattern.Concept.Attribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject1.Concept
{
    [TestClass]
    public class AttributeUnitTest
    {
        [TestMethod]
        public void Test()
        {
            IAttributedBuilder builder = new AttributedBuilder();
            Director director = new Director();
            director.BuildUp(builder);

            Assert.AreEqual<string>("b", builder.Log[0]);
            Assert.AreEqual<string>("c", builder.Log[1]);
            Assert.AreEqual<string>("a", builder.Log[2]);
        }
    }
}
