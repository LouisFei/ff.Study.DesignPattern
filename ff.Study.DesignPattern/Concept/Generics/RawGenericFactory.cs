using System;

namespace ff.Study.DesignPattern.Concept.Generics
{
    public static class RawGenericFactory
    {
        /// <summary>
        /// 动态生成实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName">
        /// TopNameSpace.SubNameSpace.ContainingClass, MyAssembly
        /// ContainingClass, MyAssembly, Version=1.3.0.0, PublicKeyToken=b17a5c561934e089
        /// </param>
        /// <returns></returns>
        public static T Create<T>(string typeName)
        {
            return (T)Activator.CreateInstance(Type.GetType(typeName));
        }
    }
}
