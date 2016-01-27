using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Common;

namespace UnitTestProject1.Common
{
    [TestClass]
    public class ConfigurationBrokerUnitTest
    {
        [TestMethod]
        public void Test()
        {
            IObjectBuilder builder = ConfigurationBroker.GetConfigurationObject<IObjectBuilder>();
            Assert.IsNotNull(builder);
        }
    }
}
