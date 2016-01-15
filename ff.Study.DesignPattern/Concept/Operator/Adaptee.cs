using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Operator
{
    public class Adaptee
    {
        /// <summary>
        /// 不兼容的接口方法
        /// </summary>
        public int Code { get { return new Random().Next(); } }
    }

    public class Target
    {
        private int data;
        public Target(int data)
        {
            this.data = data;
        }

        /// <summary>
        /// 目标接口方法
        /// </summary>
        public int Data { get { return data; } }

        /// <summary>
        /// 隐式类型转换进行适配
        /// </summary>
        /// <param name="adaptee"></param>
        /// <returns></returns>
        public static implicit operator Target(Adaptee adaptee)
        {
            return new Target(adaptee.Code);
        }
    }
}
