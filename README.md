# AopEasyLog简介
* 使用了Rougamo的库，让做了标记（LogAttribute）的方法在执行前后会有日志输出到文件
* 文件会在程序的**运行目录**下按照如下方式组织：
```
AOP
├─ {时间戳}
      ├─ MethodInvoke
      |   ├─{类名}
      |   |   ├─{实例Id}
      |   |   |   ├─{调用次数}{方法名}开始执行{时间戳}.txt
      |   |   |   ├─{调用次数}{方法名}结束执行{时间戳}.txt
      |   |   |   ├─……
      |   |   |   
      |   |   ├─{实例Id}
      |   |   |   ├─{调用次数}{方法名}开始执行{时间戳}.txt
      |   |   |   ├─{调用次数}{方法名}结束执行{时间戳}.txt
      |   |   |   ├─……
      |   |   |
      |   |   ├─{调用次数}{方法名}开始执行{时间戳}.txt
      |   |   ├─{调用次数}{方法名}开始执行{时间戳}.txt
      |   |   ├─……
      |   |
      |   └─{类名}
      |       ├─{实例Id}
      |       |   ├─{调用次数}{方法名}开始执行{时间戳}.txt
      |       |   ├─{调用次数}{方法名}结束执行{时间戳}.txt
      |       |   ├─……
      |       |
      |       ├─{实例Id}
      |       |   ├─{调用次数}{方法名}开始执行{时间戳}.txt
      |       |   ├─{调用次数}{方法名}结束执行{时间戳}.txt
      |       |   ├─……
      |       |
      |       ├─{调用次数}{方法名}开始执行{时间戳}.txt
      |       ├─{调用次数}{方法名}开始执行{时间戳}.txt
      |       ├─……
      |
      ├─ PropertyChange
          ├─{类名}
          |   ├─{实例Id}{创建的时间戳}
          |   |   ├─{属性名}
          |   |   |   ├─初始值.txt
          |   |   |   ├─{时间戳}.txt
          |   |   |   ├─{时间戳}.txt
          |   |   |   ├─……
          |   |   |
          |   |   ├─{属性名}
          |   |   |   ├─初始值.txt
          |   |   |   ├─{时间戳}.txt
          |   |   |   ├─{时间戳}.txt
          |   |   |   ├─……
          |   |   |
          |   |   ├─……
          |   |   |
          |   └─{实例Id}{创建的时间戳}
          |       ├─{属性名}
          |       |   ├─初始值.txt
          |       |   ├─{时间戳}.txt
          |       |   ├─{时间戳}.txt
          |       |   ├─……
          |       |
          |       ├─{属性名}
          |       |   ├─初始值.txt
          |       |   ├─{时间戳}.txt
          |       |   ├─{时间戳}.txt
          |       |   ├─……
          |       |
          |       ├─……
          |
          ├─{类名}
              ├─{实例Id}{创建的时间戳}
              |   ├─{属性名}
              |   |   ├─初始值.txt
              |   |   ├─{时间戳}.txt
              |   |   ├─{时间戳}.txt
              |   |   ├─……
              |   |
              |   ├─{属性名}
              |   |   ├─初始值.txt
              |   |   ├─{时间戳}.txt
              |   |   ├─{时间戳}.txt
              |   |   ├─……
              |   |
              |   ├─……
              |   
              └─{实例Id}{创建的时间戳}
                  ├─{属性名}
                  |   ├─初始值.txt
                  |   ├─{时间戳}.txt
                  |   ├─{时间戳}.txt
                  |   ├─……
                  |
                  ├─{属性名}
                  |   ├─初始值.txt
                  |   ├─{时间戳}.txt
                  |   ├─{时间戳}.txt
                  |   ├─……
                  |
                  ├─……


```
* 可以使用Release中的AOPRecordViewer进行查看

# 快速开始
首先，调用AopHelper.Init()方法，完成初始化。<br>
然后，在需要打印日志的方法上加上[Log]标记。<br>
完整的代码如下：<br>

<pre><code>
using AopEasyLog;

public class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        AopHelper.Init();
        Test();
    }

    [Log]
    static void Test()
    {
        Console.WriteLine("Test");
    }
}
</code></pre>
代码执行后会在AOP/{时间戳}/MethodInvoke/Program/下生成两个txt文档，里面记录了方法Test()开始执行和结束执行时的一些信息

# 一些配置

## AOP开关
修改AopHelper.IsOn属性可以控制AOP是否开启，当该属性修改后，可以通过Aop.OnSwitched事件来获取通知

## 打印等级
修改AopHelper.LogLevel属性可以控制AOP的打印等级<br>
[Log]特性有一个缺省参数logLevel（默认等于1），被标记的方法只有其Log的logLevel<=AopHelper.LogLevel时，才会进行响应的打印

# 整个类的方法统一打印
可以将[Log]特性写在一个类上，此时，相当于给这个类里所有的方法都指派了[Log]特性<br>
如果这个类中有部分方法不需要打印，则可以在其上添加[IgnoreMo]的特性

# 属性变化记录
如果一个类继承了IAopObject接口，可以用AopFactory.Create方法来对其进行创建<br>
在该类的对象属性发生变化时（发生PropertyChanged事件），会记录下该属性的变化<br>
如果在类上打上特性[AddINotifyPropertyChangedInterface]，则不用显式调用PropertyChanged事件（在执行set的时候会自动调用）



