using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Iterating
{
    public class ObjectWithName
    {
        private string name;

        public ObjectWithName(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }

    /// <summary>
    /// 二叉树结点
    /// </summary>
    public class BinaryTreeNode : ObjectWithName
    {
        public BinaryTreeNode(string name) : base(name)
        {
        }

        public BinaryTreeNode Left = null;
        public BinaryTreeNode Right = null;

        public IEnumerator GetEnumerator()
        {
            yield return this;

            if (Left != null)
            {
                foreach (ObjectWithName item in Left)
                {
                    yield return item;
                }
            }

            if (Right == null) yield break;
            foreach (ObjectWithName item in Right)
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// 复合迭代器
    /// </summary>
    public class CompositeIterator
    {
        /// <summary>
        /// 为每个可以遍历对象提供的容器
        /// 由于类行为object，所以CompositeIterator自身也可以嵌套
        /// </summary>
        private IDictionary<object, IEnumerator> items = new Dictionary<object, IEnumerator>();

        public void Add(object data)
        {
            items.Add(data, GetEnumerator(data));
        }

        /// <summary>
        /// 对外提供可以遍历的IEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            if (items != null && items.Count > 0)
            {
                foreach (IEnumerator item in items.Values)
                {
                    while (item.MoveNext())
                    {
                        yield return item.Current;
                    }
                }
            }
        }

        //获取IEnumerator
        public static IEnumerator GetEnumerator(object data)
        {
            if (data == null)
            {
                throw  new NullReferenceException();
            }

            Type type = data.GetType();

            //是否为Stack
            if (type.IsAssignableFrom(typeof(Stack)) || type.IsAssignableFrom(typeof(Stack<ObjectWithName>)))
            {
                return DynamicInvokeEnumerator(data);
            }

            //是否为Queue
            if (type.IsAssignableFrom(typeof (Queue)) || type.IsAssignableFrom(typeof(Queue<ObjectWithName>)))
            {
                return DynamicInvokeEnumerator(data);
            }

            //是否为Array
            if (type.IsArray && type.GetElementType().IsAssignableFrom(typeof(ObjectWithName)))
            {
                return ((ObjectWithName[]) data).GetEnumerator();
            }

            // 是否为二叉树
            if (type.IsAssignableFrom(typeof(BinaryTreeNode)))
            {
                return ((BinaryTreeNode) data).GetEnumerator();
            }

            throw  new NotSupportedException();
        }

        /// <summary>
        /// 通过反射动态调用相关实例的GetEnumerator方法获取IEnumerator
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static IEnumerator DynamicInvokeEnumerator(object data)
        {
            if (data == null)
            {
                throw new NullReferenceException();
            }

            Type type = data.GetType();

            return (IEnumerator) type.InvokeMember("GetEnumerator", BindingFlags.InvokeMethod, null, data, null);
        }

    }
}
