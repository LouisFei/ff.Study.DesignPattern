using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Common;

namespace UnitTestProject1.Common
{
    [TestClass]
    public class CacheUnitTest
    {
        [TestMethod]
        public void GenericCacheTest()
        {
            var cacheHelper = new GenericCache<string, string>();
            cacheHelper.Add("hello", "world");

            Assert.AreEqual<int>(1, cacheHelper.Count);
            Assert.AreEqual<bool>(true, cacheHelper.ContainsKey("hello"));
            Assert.AreEqual<bool>(false, cacheHelper.ContainsKey("hi"));

            var val = "";
            cacheHelper.TryGetValue("hello", out val);
            Assert.AreEqual<string>("world", val);

            cacheHelper.Clear();
            Assert.AreEqual<int>(0, cacheHelper.Count);
        }
    }
}
