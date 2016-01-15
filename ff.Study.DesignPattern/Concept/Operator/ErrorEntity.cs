using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Operator
{
    /// <summary>
    /// 可重载运算符（Overloadable Operators）
    /// 转换运算符（Conversion Operators）
    /// </summary>
    public class ErrorEntity
    {
        private IList<string> messages = new List<string>();
        private IList<int> codes = new List<int>();

        public IList<string> Messages { get { return messages; } }
        public IList<int> Codes { get { return codes; } }

        public static ErrorEntity operator +(ErrorEntity entity, string message)
        {
            entity.messages.Add(message);
            return entity;
        }

        public static ErrorEntity operator +(ErrorEntity entity, int code)
        {
            entity.codes.Add(code);
            return entity;
        }
    }
}
