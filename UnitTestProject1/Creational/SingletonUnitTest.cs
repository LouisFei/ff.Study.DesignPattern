using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.Study.DesignPattern.Creational.Singleton.Classics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UnitTestProject1.Creational
{
    [TestClass]
    public class SingletonUnitTest
    {
        [TestMethod]
        public void TestCounter()
        {
            Counter.Instance.Reset();
            Assert.AreEqual<int>(1, Counter.Instance.Next);
            Assert.AreEqual<int>(2, Counter.Instance.Next);
            Assert.AreEqual<int>(3, Counter.Instance.Next);
        }

        /// <summary>
        /// 每个线程需要执行的目标对象定义
        /// 同时在它内部完成线程内部是否 Singleton 的情况
        /// </summary>
        class Work
        {
            public static IList<int> Log = new List<int>();

            /// <summary>
            /// 每个线程的执行部分定义
            /// </summary>
            public void Procedure()
            {
                ThreadLevelSingleton s1 = ThreadLevelSingleton.Instance;
                ThreadLevelSingleton s2 = ThreadLevelSingleton.Instance;

                //证明可以在正常构造实例
                Assert.IsNotNull(s1);
                Assert.IsNotNull(s2);

                //验证当前线程执行体内部两次引用的是否为同一个实例
                Assert.AreEqual<int>(s1.GetHashCode(), s2.GetHashCode());
                //登记当前线程所使用的Singleton对象标识
                Log.Add(s1.GetHashCode());
            }

        }

        private const int ThreadCount = 3;
        [TestMethod]
        public void TestThreadLevelSingleton()
        {
            //创建一定数量的线程执行体
            Thread[] threads = new Thread[ThreadCount];
            for (int i = 0; i < ThreadCount; i++)
            {
                ThreadStart work = new ThreadStart(new Work().Procedure);
                threads[i] = new Thread(work);
            }

            //执行线程
            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            //终止线程并做其他清理工作
            //...

            //判断是否不同线程内部的单例实例是不同的
            for (int i = 0; i < ThreadCount - 1; i++)
            {
                for (int j = i + 1; j < ThreadCount; j++)
                {
                    Assert.AreNotEqual<int>(Work.Log[i], Work.Log[j]);
                }
            }
        }
    }
}
