using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Creational.FactoryMethod.ClassicsDefine.ConfigBased
{
    public class Assembler
    {
        /// <summary>
        /// 配置节名称
        /// </summary>
        private const string SectionName = "factoryMethod.customFactories";

        /// <summary>
        /// IFactory在配置文件中的键值
        /// </summary>
        private const string FactoryTypeName = "IFactory";

        /// <summary>
        /// 保存“抽象类型/具体类型”对应关系的字典
        /// </summary>
        private static Dictionary<Type, Type> dictionary = new Dictionary<Type, Type>();

        static Assembler()
        {
            //通过配置文件加载相关“抽象工厂类型”/“具体工厂类型”的映射关系
            NameValueCollection collection = (NameValueCollection)ConfigurationSettings.GetConfig(SectionName);
            for (int i = 0; i < collection.Count; i++)
            {
                string target = collection.GetKey(i);
                string source = collection[i];
                dictionary.Add(Type.GetType(target), Type.GetType(source));
            }
        }

        public T Create<T>() where T : class,IFactory
        {
            if (dictionary.ContainsKey(typeof(T)))
            {
                return (T)Activator.CreateInstance(dictionary[typeof(T)]);
            }
            else
            {
                return null;
            }
        }
    }

    public class Client
    {
        private IFactory factory;

        public Client(IFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            this.factory = factory;
        }

        public IProduct GetProduct()
        {
            return factory.Create();
        }
    }
}
