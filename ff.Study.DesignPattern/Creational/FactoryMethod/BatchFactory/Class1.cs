using ff.Study.DesignPattern.Creational.FactoryMethod.ClassicsDefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Creational.FactoryMethod.BatchFactory
{
    /// <summary>
    /// 定义产品类型容器
    /// </summary>
    public class ProductCollection
    {
        private IList<IProduct> data = new List<IProduct>();

        //对外公开的集合操作方法
        public void Insert(IProduct item)
        {
            data.Add(item);
        }

        public void Insert(IProduct[] items)
        {
            if ((items == null) || (items.Length == 0))
            {
                return;
            }

            foreach (IProduct item in items)
            {
                data.Add(item);
            }
        }

        public void Remove(IProduct item)
        {
            data.Remove(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        /// <summary>
        /// 获取所有IProduct
        /// </summary>
        public IProduct[] Data
        {
            get
            {
                if (data == null || data.Count == 0)
                {
                    return null;
                }

                IProduct[] result = new IProduct[data.Count];
                data.CopyTo(result, 0);
                return result;
            }
        }

        /// <summary>
        /// 当前集合内的元素数量
        /// </summary>
        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        //为了便于操作，重载运算符

        public static ProductCollection operator +(ProductCollection collection, IProduct[] items)
        {
            ProductCollection result = new ProductCollection();
            
            if ((collection != null) && (collection.Count > 0))
            {
                result.Insert(collection.Data);
            }

            if (items != null && items.Length > 0)
            {
                result.Insert(items);
            }

            return result;
        }

        public static ProductCollection operator +(ProductCollection source, ProductCollection target)
        {
            ProductCollection result = new ProductCollection();

            if (source != null && source.Count > 0)
            {
                result.Insert(source.Data);
            }

            if (target != null && target.Count > 0)
            {
                result.Insert(target.Data);
            }

            return result;
        }
    }


    //定义批量工厂和产品类型容器
    public interface IBatchFactory
    {
        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="quantity">待创建的产品数量</param>
        /// <returns></returns>
        ProductCollection Create(int quantity);
    }

    /// <summary>
    /// 为了方便扩展提供的抽象基类
    /// </summary>
    /// <typeparam name="T">Concrete Product 类型</typeparam>
    public class BatchProductFactoryBase<T> : IBatchFactory where T : IProduct, new()
    {
        public virtual ProductCollection Create(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException();
            }

            ProductCollection collection = new ProductCollection();
            for (int i = 0; i < quantity; i++)
            {
                collection.Insert(new T());
            }

            return collection;
        }
    }

    //两个批量生产产品的工厂类型
    public class BatchProductAFactory : BatchProductFactoryBase<ProductA> {}
    public class BatchProductBFactory : BatchProductFactoryBase<ProductB> {}

    //增设生产指导顾问——Director

    /// <summary>
    /// 决策类
    /// </summary>
    public abstract class DecisionBase
    {
        protected IBatchFactory factory;
        protected int quantity;

        public DecisionBase(IBatchFactory factory, int quantity)
        {
            this.factory = factory;
            this.quantity = quantity;
        }

        public virtual IBatchFactory Factory
        {
            get { return factory; }
        }

        public virtual int Quantity
        {
            get { return quantity; }
        }
    }

    public abstract class DirectorBase
    {
        protected IList<DecisionBase> decisions = new List<DecisionBase>();

        //实际项目中，最好将每个 Director 需要添加的 Decision 也定义在配置文件中，
        //这样增加新的 Decision 项都在后台完成，而不需要 Assembler 显式调用该方法补充。

        /// <summary>
        /// 添加决策
        /// </summary>
        /// <param name="decision"></param>
        protected virtual void Insert(DecisionBase decision)
        {
            if (decision == null || decision.Factory == null || decision.Quantity < 0)
            {
                throw new ArgumentException("decision");
            }

            decisions.Add(decision);
        }

        /// <summary>
        /// 便于客户程序使用增加的迭代器
        /// </summary>
        public virtual IEnumerable<DecisionBase> Decisions
        {
            get
            {
                return decisions;
            }
        }
    }

    //完成了三个辅助类型（ProductCollection、Director、Decision）的设计之后，就可以设计由 Director 指导生产的新客户程序。

    //下面示例采用硬编码方式，进行演示
    public class ProductADecision : DecisionBase
    {
        public ProductADecision() : base(new BatchProductAFactory(), 2) { }
    }

    public class ProductBDecision : DecisionBase
    {
        public ProductBDecision() : base(new BatchProductBFactory(), 3) { }
    }


    /// <summary>
    /// 导演类，指导客户程序
    /// </summary>
    public class ProductDirector : DirectorBase
    {
        public ProductDirector()
        {
            base.Insert(new ProductADecision());
            base.Insert(new ProductBDecision());
        }
    }

    /// <summary>
    /// 客户程序
    /// </summary>
    public class Client
    {
        //实际项目中，可以通过Assembler从外部把Director注入
        private DirectorBase director = new ProductDirector();

        public IProduct[] Produce()
        {
            ProductCollection collection = new ProductCollection();
            foreach (DecisionBase decision in director.Decisions)
            {
                collection += decision.Factory.Create(decision.Quantity);
            }

            return collection.Data;
        }
    }
}
