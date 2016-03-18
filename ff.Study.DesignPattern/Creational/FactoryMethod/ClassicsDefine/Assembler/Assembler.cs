using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Creational.FactoryMethod.ClassicsDefine.Assembler
{
    /// <summary>
    /// 通过注入实现方式把IFactory传给客户程序。
    /// </summary>
    public class Assembler
    {
        private static Dictionary<Type, Type> dictionary = new Dictionary<Type, Type>();

        static Assembler()
        {
            //注册抽象类型需要使用的实体类型
            //实际的配置信息可以从外层机制获得，例如通过配置定义
            dictionary.Add(typeof(ITimeProvider), typeof(SystemTimeProvider));
            dictionary.Add(typeof(IFactory), typeof(FactoryA));
        }
    }

    /// <summary>
    /// 客户程序/下游程序
    /// </summary>
    class Client
    {
        private IFactory factory;

        public Client(IFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            this.factory = factory;
        }

        public void AnotherMethod()
        {
            IProduct product = factory.Create();
            //......
        }
    }


    public interface ITimeProvider
    {
    }

    public class SystemTimeProvider : ITimeProvider
    {

    }
}
