using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Generics
{
    public interface ITarget { void Request(); }
    public interface IAdaptee { void SpecifiedRequest(); }

    public abstract class GenericAdapterBase<T> : ITarget where T : IAdaptee, new()
    {
        public virtual void Request()
        {
            new T().SpecifiedRequest();
        }
    }
}

/*
 Generics 在结构型和行为型模式中的作用主要还是提高算法的适用性。
 例如：Adapter完成的是接口的转换，从更抽象的角度看，就是一个Type Cast（类型转换），
 所以用泛型可以定义出一个普适的IGenericAdapter。
 */