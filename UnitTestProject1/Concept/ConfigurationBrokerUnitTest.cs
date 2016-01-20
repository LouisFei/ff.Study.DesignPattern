using ff.Study.DesignPattern.Concept.Configurating;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject1.Concept
{
    [TestClass]
    public class ConfigurationBrokerUnitTest
    {
        [TestMethod]
        public void ConfigurationTest()
        {
            DelegatingParagramConfigurationSection s1 = ConfigurationBroker.Delegating;
            Assert.IsTrue(s1.Pictures["EventHandler"].Colorized);
            Assert.AreEqual<string>("1对n的通知", s1.Examples["MulticastNotify"].Description);

            GenericsParagramConfigurationSection s2 = ConfigurationBroker.Generics;
            Assert.AreEqual<int>(1, s2.Diagrams.Count);
        }
    }
}
