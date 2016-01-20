using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Iterating
{
    public class RawIterator
    {
        private int[] data = new int[] {0, 1, 2, 3, 4};

        /// <summary>
        /// 最简单的基于数组的全部遍历
        /// 如果客户程序需要强类型的返回值，可以采用泛型IEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var item in data)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 返回某个区间内数据的IEnumerable
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable GetRange(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return data[i];
            }
        }

        /// <summary>
        /// 手工“捏”出来的IEnumerable
        /// </summary>
        public IEnumerable<string> Greeting
        {
            get
            {
                yield return "hello";
                yield return "world";
                yield return "!";
            }
        }
    }
}
