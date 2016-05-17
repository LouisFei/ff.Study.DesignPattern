#经典回顾
##使用Singleton模式的意图
* 保证一个类只有一个实例；
* 提供客户程序一个访问它的全局访问点。

##与静态的全局变量的对比
* 静态全局变量只解决了部分问题——全局访问；
* 但是没有做到限制客户程序实例化的数量。

>实际上，Singleton模式要做的就是通过控制类型实例的创建，确保后续使用的都是之前创建好的一个实例，通过这样的一个封装，客户程序就无须知道该类型实现的内部细节。

###在综合执行效率和线程同步的考虑后，我们采用一个double check的方式。
```C#
public class Singleton
{
	protected Singleton(){}
	private static volatile Singleton instance = null;
	/// Lazy方式创建唯一实例的过程
	public static Singleton Instance()
	{
		if(instance == null) //外层if
		{
			lock(typeof(Singleton)) //多线程中共享资源同步
			{
				if(instance == null) //内层if
				{
					instance = new Singleton();
				}
			}
		}
	}
}
```

##线程安全的Singleton
```C#
class Singleton
{
	private Singleton(){}
	public static readonly Singleton Instance = new Singleton();
}
```