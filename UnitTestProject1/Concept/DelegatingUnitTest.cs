using System.Threading;
using ff.Study.DesignPattern.Concept.Delegating;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.Concept
{
    [TestClass]
    public class DelegatingUnitTest
    {
        [TestMethod]
        public void AsyncInvokerTest()
        {
            AsyncInvoker asyncInvoker = new AsyncInvoker();
            Thread.Sleep(3000);
            Assert.AreEqual<string>("method", asyncInvoker.Output[0]);
            Assert.AreEqual<string>("fast", asyncInvoker.Output[1]);
            Assert.AreEqual<string>("slow", asyncInvoker.Output[2]);
        }

        [TestMethod]
        public void InvokeListTest()
        {
            string message = string.Empty;
            InvokeList list = new InvokeList();
            list.Invoke();
            Assert.AreEqual<string>("hello,world", list[0] + list[1] + list[2]);
        }

        [TestMethod]
        public void MulticastDelegateTest()
        {
            MulticasDelegateInvoker invoker = new MulticasDelegateInvoker();
            Assert.AreEqual<string>("hello,world", invoker[0] + invoker[1] + invoker[2]);
        }

        [TestMethod]
        public void AnonymousMethodTest()
        {
            AnonymousMethod invoker = new AnonymousMethod();
            Assert.AreEqual<string>("hello,world", invoker[0] + invoker[1] + invoker[2]);
        }
    }
}
