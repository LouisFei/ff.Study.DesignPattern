using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ff.Study.DesignPattern.Concept.Iterating;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1.Concept
{
    [TestClass]
    public class IteratingUnitTest
    {
        [TestMethod]
        public void RawIteratorTest()
        {
            // 测试 IEnumerator
            int count = 0; 
            RawIterator iterator = new RawIterator();
            foreach (int item in iterator)
            {
                Assert.AreEqual<int>(count++, item);
            }

            //测试具有参数控制的IEnumerable
            count = 1; 
            foreach (int item in iterator.GetRange(1,3))
            {
                Assert.AreEqual<int>(count++, item);
            }

            //测试手工捏出来的IEnumerable
            string[] data = new string[] {"hello", "world", "!"};
            count = 0;
            foreach (string item in iterator.Greeting)
            {
                Assert.AreEqual<string>(data[count++], item);
            }
        }

        [TestMethod]
        public void CompositeIteratorTest()
        {
             Stack<ObjectWithName> stack = new Stack<ObjectWithName>();
            stack.Push(new ObjectWithName("2"));
            stack.Push(new ObjectWithName("1"));

            Queue<ObjectWithName> queue = new Queue<ObjectWithName>();
            queue.Enqueue(new ObjectWithName("3"));
        }
    }
}
