using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Indexer
{
    /*
     通过委托传递索引规则
     对于检索规则固定的情况而言，我们可以通过在索引器内部硬编码完成。
     但如果要完成一些更为公共的类库，往往还要“授之以渔”，即除了告诉它“要检索”之外，还要告知检索策略和规则。
     在这方面，C#提供了对象化的托管委托类型（delegate），可以善加利用。
     这时候，我们会发现索引器的功能更加强大，就像在使用SQL语句的WHERE子句一样，根据需要以灵活的方式对目标数据进行筛选。
     */
    
    public class Dashboard
    {
        float[] temps = new float[10] { 56.2F, 56.7F, 56.5F, 56.9F, 58.8F, 61.3F, 65.9F, 62.1F, 59.2F, 57.5F };

        /// <summary>
        /// 与SQL语句中Where子句的效果非常类似
        /// </summary>
        /// <param name="predicate">传入的检索规则</param>
        /// <returns></returns>
        public float this[Predicate<float> predicate]
        {
            get
            {
                float[] matches = Array.FindAll<float>(temps, predicate);
                return matches[0];
            }
        }
    }
}

/*
 索引器：承担各种检索和查找的工作。
 属性（Property）：承担“它的……特性是……”或“它们的……特质是……”的工作，用来标注某个实例特性（成品属性）或静态特性（静态属性）。
 普通的方法：承担“让它处理……”的职能。
 事件定位于：“当……发生的时候，要做些……”。
 */