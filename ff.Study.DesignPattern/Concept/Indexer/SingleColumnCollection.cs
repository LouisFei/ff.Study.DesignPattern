using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Indexer
{
    public class SingleColumnCollection
    {
        private static string[] countries = new string[] { "china", "chile", "uk" };

        public string this[int index] { get { return countries[index]; } }

        public string[] this[string name]
        {
            get
            {
                if(countries == null || countries.Length <= 0)
                {
                    return null;
                }
                else
                {
                    return Array.FindAll<string>(countries,
                         delegate(string candicate)
                         {
                             return candicate.StartsWith(name);
                         }
                     );
                }
            }
        }
    }
}
