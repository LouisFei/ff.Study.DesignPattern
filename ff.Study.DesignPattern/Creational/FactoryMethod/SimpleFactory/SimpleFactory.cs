using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Creational.FactoryMethod.SimpleFactory
{
    public interface IProduct { }
    public class ConcreteProductA : IProduct { }

    public class ConcreteProductB : IProduct { }

    /// <summary>
    /// 简单工厂
    /// </summary>
    public class SimpleFactory
    {
        public IProduct Create()
        {
            // 工厂决定到底实例化哪个子类
            return new ConcreteProductA();
        }
    }

    public enum Category
    {
        A,
        B
    }

    /// <summary>
    /// 参数化工厂
    /// </summary>
    public static class ParametricProductFactory
    {
        public static IProduct Create(Category category)
        {
            switch (category)
            {
                case Category.A:
                    return new ConcreteProductA();
                case Category.B:
                    return new ConcreteProductB();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}

/*
 通过IProduct隔离了客户程序与具体ConcreteProductX的依赖关系，在客户程序视野内根本就没有ConcreteProcutX。
 即使ConcreteProductX增加、删除方法或属性，也无妨大局，只要按照要求实现了IProduct就可以，Client无须关心ConcreteProductX的变化（确切地说它也关心不着，因为看不到）。
 */
