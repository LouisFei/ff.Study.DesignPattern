using ff.Study.DesignPattern.Concept.DependencyInjection.Example3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1.Concept
{
    [TestClass]
    public class DependencyInjectionUnitTest
    {
        [TestMethod]
        public void ConstructorDiTest()
        {
            ITimeProvider timeProvider = new Assembler().Create<ITimeProvider>();
            Assert.IsNotNull(timeProvider); //确认可以正常获得抽象类型实例

            ClientUseConstructorDi client = new ClientUseConstructorDi(timeProvider); //在构造函数中注入
        }

        [TestMethod]
        public void SetterDiTest()
        {
            ITimeProvider timeProvider = new Assembler().Create<ITimeProvider>();
            Assert.IsNotNull(timeProvider); //确认可以正常获得抽象类型实例

            ClientUseSetterDi client = new ClientUseSetterDi();
            client.TimeProvider = timeProvider; //通过Setter实现注入
        }

        [TestMethod]
        public void InterfaceDiTest()
        {
            ITimeProvider timeProvider = new Assembler().Create<ITimeProvider>();
            Assert.IsNotNull(timeProvider); //确认可以正常获得抽象类型实例

            IObjectWithTimeProvider client = new ClientUseInterfaceDi();
            client.TimeProvider = timeProvider; //通过接口实现注入
        }

        [TestMethod]
        public void AttributeDiTest()
        {
            ClientUseAttribute client = new ClientUseAttribute();
            Assert.IsTrue(client.GetYear() > 0);
        }
    }
}
