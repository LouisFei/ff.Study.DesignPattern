using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Delegating
{
    public delegate void MemoHandler(int x, int y, IDictionary<string, int> data);

    /// <summary>
    /// 对具有重载的多个目标方法Delegate
    /// </summary>
    public class OverloadableDelegateInvoker
    {
        private MemoHandler handler;

        public OverloadableDelegateInvoker()
        {
            Type type = typeof(MemoHandler);
            Delegate d = Delegate.CreateDelegate(type, new C1(), "A");
            d = Delegate.Combine(d, Delegate.CreateDelegate(type, new C2(), "S"));
            d = Delegate.Combine(d, Delegate.CreateDelegate(type, new C3(), "M"));
            handler = (MemoHandler)d;
        }

        public void Memo(int x, int y, IDictionary<string, int> data)
        {
            handler(x, y, data);
        }
    }

    public class C1
    {
        public void A(int x, int y, IDictionary<string, int> data)
        {
            data.Add("A", x + y);
        }
    }

    public class C2
    {
        public void S(int x, int y, IDictionary<string, int> data)
        {
            data.Add("S", x - y);
        }
    }

    public class C3
    {
        public void M(int x, int y, IDictionary<string, int> data)
        {
            data.Add("M", x * y);
        }
    }
}

/*
 Delegate自身就具有组合特征，可以通过组合加组播的方式，自动代理客户程序对多个目标方法的抽象调用。
 */
