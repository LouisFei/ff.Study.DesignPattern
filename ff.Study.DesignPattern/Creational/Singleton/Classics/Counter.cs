using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Creational.Singleton.Classics
{
    public class Counter
    {
        private Counter() { }

        //static成员会优先初始化。
        //readonly成员只能在定义的时候和构造函数里赋值。
        public static readonly Counter Instance = new Counter();

        private int value;

        public int Next
        {
            get
            {
                return ++value;
            }
        }

        public void Reset()
        {
            value = 0;
        }
    }
}
