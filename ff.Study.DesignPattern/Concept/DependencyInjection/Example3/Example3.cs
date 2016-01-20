using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.DependencyInjection.Example3
{
    /*
     设计模式中，尤其是结构型模式很多时候解决的就是对象间的依赖关系，变依赖具体为依赖对象。
     平时开发中如果发现客户程序依赖某个（或某类）对象，我们常常会对它们进行一次抽象，形成抽象的抽象类、接口，这样客户程序就可以摆脱所依赖的具体类型。
     这个过程中有个环节，谁来选择客户程序需要的满足抽象类型的具体类型呢？
     “依赖注入”的方式将加工好的抽象类型实例“注入”到客户程序中。
     Martin Fowler提到了三种经典方式，分别是：通过（构造函数、Setter、接口）实现注入。
     依赖注入，被Martin Fowler称为一个模式，但平时使用中，它更多地作为一项实现技巧出现，
     开发中很多时候需要借助这项技巧把各个设计模式所加工的成果传递给客户程序。
     各种实现方式虽然最终目标一致，但在使用特性上有很多区别。
     * 
     */

    public interface ITimeProvider
    {
        DateTime CurrentDate { get; }
    }

    public class TimeProvider : ITimeProvider
    {
        public DateTime CurrentDate
        {
            get { return DateTime.Now; }
        }
    }

    /// <summary>
    /// 需要增加一个对象，由它选择某种方式把ITimeProvider实例传递给客户程序，这个对象被称为Assembler.
    /// Assembler的职责如下：
    /// 1、知道每个具体类型。
    /// 2、可根据客户程序的需要，将抽象类型反馈给客户程序。
    /// 3、本身还负责对具体类型的对象的创建。
    /// </summary>
    public class Assembler
    {
        /// <summary>
        /// 保存“抽象类型/实体类型”对应关系的字典
        /// </summary>
        private static Dictionary<Type, Type> dictionary = new Dictionary<Type, Type>();

        static Assembler()
        {
            // 注册抽象类型需要使用的实体类型
            // 实际的配置信息可以从外层机制获得，例如通过配置定义
            dictionary.Add(typeof(ITimeProvider), typeof(TimeProvider));
        }

        /// <summary>
        /// 根据客户程序需要的抽象类型选择相应的实体类型，并返回类型实例。
        /// </summary>
        /// <param name="type">抽象类型（抽象类/接口/或者某种基类）</param>
        /// <returns>实体类型实例</returns>
        public object Create(Type type) //主要用于非泛型方式调用
        {
            if (type == null || !dictionary.ContainsKey(type))
            {
                throw new NullReferenceException();
            }

            Type targetType = dictionary[type];
            return Activator.CreateInstance(targetType);
        }

        /// <summary>
        /// 主要用于泛型方式调用
        /// </summary>
        /// <typeparam name="T">抽象类型（抽象类/接口/或者某种基类）</typeparam>
        /// <returns></returns>
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }
    }

    #region Constructor 注入
    /// <summary>
    /// Constructor 注入
    /// 在构造函数中注入
    /// </summary>
    public class ClientUseConstructorDi
    {
        private ITimeProvider timeProvider;

        public ClientUseConstructorDi(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
        }
    }
    #endregion

    #region Setter实现注入
    /// <summary>
    /// Setter实现注入
    /// </summary>
    public class ClientUseSetterDi
    {
        private ITimeProvider timeProvider;

        public ITimeProvider TimeProvider
        {
            get { return this.timeProvider; }
            set { this.timeProvider = value; }
        }
    }
    #endregion

    #region 接口注入
    /// <summary>
    /// 定义需要注入ITimeProvider的类型
    /// </summary>
    public interface IObjectWithTimeProvider
    {
        ITimeProvider TimeProvider { get; set; }
    }

    /// <summary>
    /// 通过接口方式注入
    /// </summary>
    public class ClientUseInterfaceDi : IObjectWithTimeProvider
    {
        private ITimeProvider timeProvider;

        public ITimeProvider TimeProvider
        {
            get { return this.timeProvider; }
            set { this.timeProvider = value; }
        }
    }
#endregion

    #region 基于Attribute实现注入

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class DecoratorAttribute : System.Attribute
    {
        /// <summary>
        /// 实现客户类型实际需要的抽象类型的实体类型实例，即注入客户类型的内容
        /// </summary>
        public readonly object Injector;
        private Type type;

        public DecoratorAttribute(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            this.type = type;
            Injector = new Assembler().Create(this.type);
        }

        /// <summary>
        /// 客户类型需要的抽象对象类型
        /// </summary>
        public Type Type { get { return this.type; } }
    }

    /// <summary>
    /// 帮助客户类型和客户程序获得其Attribute定义中需要的抽象类型实例的工具类
    /// </summary>
    public static class AttributeHelper
    {
        public static T Injector<T>(object target) where T : class
        {
            if(target == null)
            {
                throw new ArgumentNullException("target");
            }

            Type targetType = target.GetType();
            object[] attributes = targetType.GetCustomAttributes(typeof(DecoratorAttribute), false);
            if (attributes == null || attributes.Length <= 0)
            {
                return null;
            }

            foreach (DecoratorAttribute attribute in (DecoratorAttribute[])attributes)
            {
                if (attribute.Type == typeof(T))
                {
                    return (T)attribute.Injector;
                }
            }

            return null;
        }
    }

    /// <summary>
    /// 应用Attribute，定义需要将ITimeProvider通过它注入
    /// </summary>
    [Decorator(typeof(ITimeProvider))]
    public class ClientUseAttribute
    {
        public int GetYear()
        {
            // 与其他方式注入不同的是，这里使用的ITimeProvider来自自己的Attribute
            ITimeProvider provider = AttributeHelper.Injector<ITimeProvider>(this);
            return provider.CurrentDate.Year;
        }

    }

    #endregion
}
