using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Delegating
{
    public class AnonymousMethod
    {
        private string[] message = new string[3];

        public AnonymousMethod()
        {
            StringAssignmentEventHandler handler = null;
            handler += delegate { message[0] = "hello"; };
            handler += delegate { message[1] = ","; };
            handler += delegate { message[2] = "world"; };
            handler.Invoke();
        }

        public string this[int index] { get { return message[index]; } }
    }
}

/*
 调用匿名方法
 方法，也就是一小段可以重用的处理过程，一般我们都会把它独立编写出来，
 */