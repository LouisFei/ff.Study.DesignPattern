using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Creational.Singleton.Classics
{
    /// <summary>
    /// 桌面应用中线程级颗粒度的单例
    /// 对于Windows Forms，可以通过System.ThreadStaticAttribute比较容易地告诉CLR其中的静态唯一属性Instance仅在本线程内部静态。
    /// </summary>
    public class ThreadLevelSingleton
    {
        private ThreadLevelSingleton() { }

        [ThreadStatic]
        private static ThreadLevelSingleton instance;

        public static ThreadLevelSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ThreadLevelSingleton();
                }

                return instance;
            }
        }
    }
}
