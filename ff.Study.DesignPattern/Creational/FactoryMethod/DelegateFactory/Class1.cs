using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ff.Study.DesignPattern.Creational.FactoryMethod.GenericFactory;

namespace ff.Study.DesignPattern.Creational.FactoryMethod.DelegateFactory
{
    //委托本质上就是对具体执行方法的抽象，它相当于 product 的角色
    public delegate int CalculateHandler (params int[] items);

    public class Calculator
    {
        //这个方法相当于 Delegate Factory 看到的 Concrete Product，具体产品
        public int Add(params int[] items)
        {
            int result = 0;
            foreach (var item in items)
            {
                result += item;
            }

            return result;
        }
    }

    //具体工厂  Concrete Factory
    public class CalculateHandlerFactory : IFactory<CalculateHandler>
    {
        public CalculateHandler Create()
        {
            return new Calculator().Add;
        }
    }
}
