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

        [TestMethod]
        public void FindStaffTest()
        {
            Staff staff = new Staff();
            Employee employee = staff["John", "Doe"];
            string exptected = "Vice President";
            Assert.AreEqual<string>(exptected, employee.Title);
        }

        [TestMethod]
        public void DelegateIndexRuleTest()
        {
            float expected = 65.9F;
            Dashboard dashboard = new Dashboard();
            float actual = dashboard[
                delegate(float data)   // Predicate<float>的委托
                {
                    return data > 63F;
                }];
            Assert.AreEqual<float>(expected, actual);

            expected = 56.7F;
            actual = dashboard[
                delegate(float data)    // 更换规则
                {
                    return (data < 63F) && (data > 56.5F);
                }];
            Assert.AreEqual<float>(expected, actual);
        }
    }
}
