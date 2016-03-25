using ff.Study.DesignPattern.Creational.FactoryMethod.BatchFactory;
using ff.Study.DesignPattern.Creational.FactoryMethod.ClassicsDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Creational.FactoryMethod.GenericFactory
{
    /// <summary>
    /// 泛型工厂接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFactory<T>
    {
        T Create();
    }

    /// <summary>
    /// 抽象的泛型工厂基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FactoryBase<T> : IFactory<T> where T : new()
    {
        /// <summary>
        /// 由于批量工厂的可能应用概率比较小，因此默认为实现单个产品的工厂
        /// </summary>
        /// <returns></returns>
        public virtual T Create()
        {
            return new T();
        }
    }

    //生产单个产品的实体工厂
    public class ProductAFactory : FactoryBase<ProductA> { }
    public class ProductBFactory : FactoryBase<ProductB> { }

    /// <summary>
    /// 生产批量产品工厂的抽象定义
    /// </summary>
    /// <typeparam name="TCollection"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public abstract class BatchFactoryBase<TCollection, TItem> : FactoryBase<TCollection> 
        where TCollection : ProductCollection, new ()
        where TItem : IProduct, new()
    {
        protected int quantity;
        public virtual int Quantity
        {
            set { this.quantity = value; }
        }

        public override TCollection Create()
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("quantity");
            }

            TCollection collection = new TCollection();
            for (int i = 0; i < quantity; i++)
            {
                collection.Insert(new TItem());
            }

            return collection;
        }
    }

    //生产批量产品的具体工厂
    public class BatchProductAFactory : BatchFactoryBase<ProductCollection, ProductA> { }
    public class BatchProductBFactory : BatchFactoryBase<ProductCollection, ProductB> { }

    //上述设计的好处在于：
    //1、整个体系只有一个根对象IFactory<T>，整个体系统一。
    //2、另一个好处是隐性的，如果项目规模比较大，常常会感到别扭的地方就在于实际加工方法的命名，
    //   如果不做任何约束，开发人员可以写出各种样式，与其“大杂烩”，不如大家都规规矩矩地用一个统一的名称，当然，如果子类有特殊情况，可以自己增加，但最基本的方法名称大家都用一个。
    //3、如果您设计的是某种框架（也就是具有常说的那种IOC特质——“Don't call me, I'll call you”的类库），而且还常常会用到反射调用某个工厂生产产品，
    //   那么统一名称可以令您写出一个很通用的反射发现Create()的机制，否则这个事情就麻烦了。


}
