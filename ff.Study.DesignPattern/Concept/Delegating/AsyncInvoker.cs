using System.Collections.Generic;
using System.Threading;

namespace ff.Study.DesignPattern.Concept.Delegating
{
    public class AsyncInvoker
    {
        // 记录异步执行的结果
        private IList<string> outputList = new List<string>();

        public AsyncInvoker()
        {
            Timer slowTimer = new Timer(new TimerCallback(OnTimerInterval), "slow", 2500, 2500);
            Timer fastTimer = new Timer(new TimerCallback(OnTimerInterval), "fast", 2000, 2000);

            outputList.Add("method");
        }

        private void OnTimerInterval(object state)
        {
            outputList.Add(state as string);
        }

        public IList<string> Output
        {
            get { return outputList; }
        }
    }
}
/*
 TimerCallback是一个Delegate，它定义了每个Timer触发时需要回调的方法。
 由于它与主流程间是异步执行的，因此从测试结果看，主流程首先执行完成，而两个快慢Timer则先后执行。
 
 除此之外，这个事例还表达了一个非常重要的意图：
 Delegate是对具体方法的抽象，它屏蔽了Delegate的调用者与实际执行方法间的关联关系。
 例如上例中调用者是Timer，而执行方法是某个AsyncInvoker实例的OnTimerInterval方法。
 这个特性可用于行为型模式中，采用Delegate这种抽象的操作方法表示。
*/
