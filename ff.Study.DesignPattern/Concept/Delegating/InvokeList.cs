using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Delegating
{
    /// <summary>
    /// 抽象的操作方法
    /// </summary>
    public delegate void StringAssignmentEventHandler();

    public class InvokeList
    {
        private IList<StringAssignmentEventHandler> handlers;
        private string[] message = new string[3];

        public InvokeList()
        {
            //绑定一组抽象方法
            handlers = new List<StringAssignmentEventHandler>();
            handlers.Add(AppendHello);
            handlers.Add(AppendComma);
            handlers.Add(AppendWorld);
        }

        public void Invoke()
        {
            foreach (StringAssignmentEventHandler handler in handlers)
            {
                handler();
            }
        }

        public string this[int index] { get { return message[index]; } }

        //具体操作方法
        public void AppendHello() { message[0] = "hello";}
        public void AppendComma() { message[1] = ","; }
        public void AppendWorld() { message[2] = "world"; }
    }
}

/*
 对n的通知
 通过Delegate集合可以实现一个对象与多个抽象方法的1:1:n（调用者:Delegate集合:抽象方法）的关系。
 */