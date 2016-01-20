using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.DependencyInjection.Example1
{
    public class TimeProvider
    {
        public DateTime CurrentDate { get { return DateTime.Now; } }
    }

    public class Client
    {
        public int GetYear()
        {
            TimeProvider timeProvier = new TimeProvider();
            return timeProvier.CurrentDate.Year;
        }
    }
}
