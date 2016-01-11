using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Attribute
{
    /// <summary>
    /// Builder抽象行为定义
    /// </summary>
    public interface IAttributedBuilder
    {
        /// <summary>
        /// 记录Builder的执行情况
        /// </summary>
        IList<string> Log { get; }

        void BuildPartA();
        void BuildPartB();
        void BuildPartC();
    }

    [Director(3, "BuildPartA")]
    [Director(1, "BuildPartB")]
    [Director(2, "BuildPartC")]
    public class AttributedBuilder : IAttributedBuilder
    {
        private IList<string> log = new List<string>();
        public IList<string> Log { get { return log; } }

        public void BuildPartA() { log.Add("a"); }
        public void BuildPartB() { log.Add("b"); }
        public void BuildPartC() { log.Add("c"); }
    }

    /// <summary>
    /// 通过attribute扩展Director
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class DirectorAttribute : System.Attribute, IComparable<DirectorAttribute>
    {
        //执行优先级
        private int priority;
        private string method;

        public DirectorAttribute(int priority, string method)
        {
            this.priority = priority;
            this.method = method;
        }

        public int Priority { get { return this.priority; } }
        public string Method { get { return this.method;     } }

        /// <summary>
        /// 提供按照优先级比较的ICompare<T>实现，由于Array.Sort<T>实际是升序排列，而Array.Reverse是完全反转，
        /// 因此这里调整了比较的方式为“输入参数优先级-当前实例优先级”
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DirectorAttribute other)
        {
            return this.priority - other.priority;
        }
    }

    public class Director
    {
        public void BuildUp(IAttributedBuilder builder)
        {
            //获取Builder的DirectorAttribute属性
            object[] attributes = builder.GetType().GetCustomAttributes(typeof(DirectorAttribute), false);
            if(attributes.Length <= 0)
            {
                return;
            }

            DirectorAttribute[] directors = new DirectorAttribute[attributes.Length];

            for (int i = 0; i < attributes.Length; i++)
            {
                directors[i] = (DirectorAttribute)attributes[i];
            }

            //按每个DirectorAttribute优先级逆序排序后，逐个执行
            Array.Sort<DirectorAttribute>(directors);
            foreach (DirectorAttribute attribute in directors)
            {
                InvokeBuildPartMethod(builder, attribute);
            }
        }

        /// <summary>
        /// helper method: 按照DirectorAttribute的要求，执行相关的Builder方法
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="attribute"></param>
        private void InvokeBuildPartMethod(IAttributedBuilder builder, DirectorAttribute attribute)
        {
            switch (attribute.Method)
            {
                case "BuildPartA": builder.BuildPartA(); break;
                case "BuildPartB": builder.BuildPartB(); break;
                case "BuildPartC": builder.BuildPartC(); break;
            }
        }
    }
}

/*
 用作标签的方式扩展对象特性——属性（Attribute）
 基于属性的编码完全站在实际逻辑外面，
 如果说经典设计模式中Decorator通过套接在不生成子类的情况下为类添加职责，
 那么Attribute则通过一个更简洁的方法为类“装饰”出职责和特性的机制。
 
 用Attribute指导模式。
 将Director指导Builder组装的每个步骤通过DirectorAttribute属性类来表示，
 而Director在BuildUp的过程中，通过反射获得相关Builder的DirectorAttribute列表，对列表
 进行优先排序后，执行每个DirectorAttribute指向的Build Part方法。
 
 */