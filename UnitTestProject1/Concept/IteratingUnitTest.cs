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
            #region 准备测试数据

            // Stack<T> 堆栈
            Stack<ObjectWithName> stack = new Stack<ObjectWithName>();
            stack.Push(new ObjectWithName("2"));
            stack.Push(new ObjectWithName("1"));

            // Queue<T> 队列
            Queue<ObjectWithName> queue = new Queue<ObjectWithName>();
            queue.Enqueue(new ObjectWithName("3"));
            queue.Enqueue(new ObjectWithName("4"));

            // T[] 数组
            ObjectWithName[] array = new ObjectWithName[3];
            array[0] = new ObjectWithName("5");
            array[1] = new ObjectWithName("6");
            array[2] = new ObjectWithName("7");

            #region BinaryTree 二叉树
            BinaryTreeNode root = new BinaryTreeNode("8");
            root.Left = new BinaryTreeNode("9");
            root.Right = new BinaryTreeNode("10");

            root.Right.Left = new BinaryTreeNode("11");
            root.Right.Left.Left = new BinaryTreeNode("12");
            root.Right.Left.Right = new BinaryTreeNode("13");

            root.Right.Right = new BinaryTreeNode("14");
            root.Right.Right.Right = new BinaryTreeNode("15");
            root.Right.Right.Right.Right = new BinaryTreeNode("16");
            #endregion

            // 合并所有 IEnumerator 对象
            CompositeIterator iterator = new CompositeIterator();
            iterator.Add(stack);
            iterator.Add(queue);
            iterator.Add(array);
            iterator.Add(root);

            #endregion

            int count = 0;
            foreach (ObjectWithName obj in iterator)
            {
                Assert.AreEqual<string>((++count).ToString(), obj.ToString());
            }

            /**
             * 无论您所面对的对象内部结构多么复杂，如果您的任务是封装它们，而不只是把它们全部public了，
             * 则尽可能地提供一个Iterator，可以的话按照需要使用的领域各提供一个Iterator。
             */
        }
    }
}
