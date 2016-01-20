using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.DependencyInjection.Example2
{
    public interface ITimeProvider
    {
        DateTime CurrentDate { get; }
    }

    public class TimeProvider : ITimeProvider
    {
        public DateTime CurrentDate
        {
            get { return DateTime.Now; }
        }
    }

    public class Client
    {
        public int GetYear()
        {
            ITimeProvider timeProvider = new TimeProvider();
            return timeProvider.CurrentDate.Year;
        }
    }

}
