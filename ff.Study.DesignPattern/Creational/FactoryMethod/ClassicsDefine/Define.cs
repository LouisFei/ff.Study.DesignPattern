using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Creational.FactoryMethod.ClassicsDefine
{
    //经典工厂方法模式的静态结构

    /// <summary>
    /// 抽象产品类型
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// 约定的抽象产品所必须具有的特征
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// 具体产品类型
    /// </summary>
    public class ProductA : IProduct
    {
        public string Name
        {
            get { return "A"; }
        }
    }

    /// <summary>
    /// 具体产品类型
    /// </summary>
    public class ProductB : IProduct
    {
        public string Name
        {
            get { return "B"; }
        }
    }

    /// <summary>
    /// 抽象的工厂类型描述
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// 每个工厂所需要具有的工厂方法——创建产品
        /// </summary>
        /// <returns></returns>
        IProduct Create();
    }

    /// <summary>
    /// 具体的工厂类型
    /// </summary>
    public class FactoryA : IFactory
    {
        public IProduct Create()
        {
            return new ProductA();
        }
    }

    /// <summary>
    /// 具体的工厂类型
    /// </summary>
    public class FactoryB : IFactory
    {
        public IProduct Create()
        {
            return new ProductB();
        }
    }
}
