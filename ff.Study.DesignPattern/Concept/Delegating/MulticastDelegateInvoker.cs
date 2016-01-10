using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Delegating
{
    public class MulticasDelegateInvoker
    {
        private string[] message = new string[3];

        public MulticasDelegateInvoker()
        {
            StringAssignmentEventHandler handler = null;
            handler += new StringAssignmentEventHandler(AppendHello);
            handler += new StringAssignmentEventHandler(AppendComma);
            handler += new StringAssignmentEventHandler(AppendWorld);
            handler.Invoke();
        }

        public string this[int index] { get { return message[index]; } }

        //具体操作方法
        public void AppendHello() { message[0] = "hello";}
        public void AppendComma() { message[1] = ","; }
        public void AppendWorld() { message[2] = "world"; }
    }
}

/*
 delegate声明的Delegate类型其实本身继承自System.MulticastDelegate，从名字上不难发现它表示广播，
 也就是它的调用列表中可以拥有多个委托，同时它重载了“+=”和“-=”运算符以便于使用。 
 */