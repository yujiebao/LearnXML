# 1.XML语法

## 1.<?xml version = "1.0" encoding = "UTF-8"?> 

version代表版本 encoding代表编码格式(读取文件时，解析字符串使用的编码格式) 

必须写在第一句

## 2.注释

<--

中间是注释的内容

-->

## 3.规则

必须有一个根节点  XML为树形结构 树根   有根才能查找

## 4.节点和属性

属性和元素节点只是写法上的区别而已 我们可以选择自己喜欢的方式来记录数据

```xml
<?xml version="1.0" encoding="utf-8"?>
<TestLesson3>
  <test1 num ="1">
    <int>0</int>
  </test1>
  <test2>
    <string>test2</string>
  </test2>
</TestLesson3>
```

# 2.C#加载XML

## 1.C#读取XML的方法有几种

1.XmlDocument  (把数据加载到内存中，方便读取)

2.XmlTextReader(以流形式加载，内存占用更少，但是是单向只读，使用不是特别方便，除非有特殊需求，否则不会使用)

3.Linq

## 2.读取XML文件信息 (使用XmlDocument 进行读取)

### 1.直接根据xml字符串内容 来加载xml文件

```csharp
XmlDocument xml = new XmlDocument();
        //存放在Resorces文件夹下的xml文件加载处理
        TextAsset textAsset =  Resources.Load<TextAsset>("Test");
        print(textAsset.text);
        xml.LoadXml(textAsset.text);
        //将一个 XML 格式的字符串加载到 XmlDocument 对象中，从而可以对这个 XML 数据进行解析、读取和操作。
```

### 2.通过xml文件的路径去进行加载

        ```C#
xml.Load(Application.streamingAssetsPath + "/Test.xml");
        ```

## 3.读取元素和属性信息 

### 1.读取使用的类

1.节点信息类

XmlNode 单个节点信息类

2.节点列表信息

XmlNodeList   多个节点信息类

### 2.获取元素和属性

#### 1.获取XML中的数据的第一步:获取XML的根节点

```csharp
 //获取xml中的根节点
 XmlNode root = xml.SelectSingleNode("Root");
 //再通过根节点 去获取下面的子节点
 XmlNode nodeName = root.SelectSingleNode("name");
```

#### 2.获取元素

```C#
 //如果想要获取节点包裹的元素 直接 .InnerText
 print(nodeName.InnerText);

```

#### 3.获取属性

```c#
//属性的获取
XmlNode nodeItem = root.SelectSingleNode("Item");
//第一种 通过 中括号获取信息
print(nodeItem.Attributes["id"].Value);
print(nodeItem.Attributes["num"].Value);
//第二种 通过属性名称获取信息
print(nodeItem.Attributes.GetNamedItem("id").Value);
```

### 3.获取多个节点

```C#
//获取一个节点下所有的同名节点的方法
XmlNodeList nodeList = root.SelectNodes("Friend");
//1.通过迭代器遍历
foreach (XmlNode node in nodeList)
{
    print(node.SelectSingleNode("name").InnerText);
    print(node.SelectSingleNode("age").InnerText);
}
//2.双层for循环
print("");
for (int i = 0; i < nodeList.Count; i++)
{
    XmlNode node = nodeList[i];
    print(node.SelectSingleNode("name").InnerText);
    print(node.SelectSingleNode("age").InnerText);
}
```

# 3.C#存储XML

## 1.使用的关键类

 XmlDecument 用于创建节点 存储文件
XmlDeclaration 用于添加版本信息
XmlElement 节点类

## 2.存储XML文件(5步)

```C#
//1.创建文本对象
XmlDocument xml = new XmlDocument();
        
//2.添加固定版本信息
//这句代码相当于是创建   <?version = "1.0" encoding = "UTF-8"?>  
XmlDeclaration xmlDec = xml.CreateXmlDeclaration("1.0","utf-8","");
//创建完成后要添加到xml中
xml.AppendChild(xmlDec);
        
//3.添加节点
XmlElement root = xml.CreateElement("PlayerInfo");
xml.AppendChild(root);
//4.为根节点添加子节点
        
//加一个name子节点
XmlElement name = xml.CreateElement("name");
name.InnerText = "小明";
root.AppendChild(name);
XmlElement atk = xml.CreateElement("atk");
atk.InnerText = "100";
root.AppendChild(atk);
XmlElement listInt = xml.CreateElement("ListInt");
for(int i = 0; i < 3; i++)
{
    XmlElement childNode = xml.CreateElement("int");
    childNode.InnerText = i.ToString();
    listInt.AppendChild(childNode); 
}
root.AppendChild(listInt);
//添加属性
XmlElement ItemList = xml.CreateElement("ItemList");
for(int i = 0; i < 3; i++)
{
    XmlElement childNode = xml.CreateElement("Item");
    //属性
    childNode.SetAttribute("id",i.ToString());
    childNode.SetAttribute("num",(i*10).ToString());
    ItemList.AppendChild(childNode);
}
root.AppendChild(ItemList);
//5.保存
xml.Save(path);
```

## 3.修改XML文件

```C#
//1.先判断是否存在文件
if(File.Exists(path))
{
    //2.加载后 直接添加节点 移除节点即可
    XmlDocument newxml = new XmlDocument();
    newxml.Load(path);
    //修改就是在原有文件基础上 去移除 或者添加
    //移除
    XmlNode Root =  newxml.SelectSingleNode("PlayerInfo");
    XmlNode atkData = Root.SelectSingleNode("atk");
    // XmlNode atkData2 =  newxml.SelectSingleNode("PlayerInfo/atk");   和上面的两句一样  通过"/"来简化父子关系 
    Root.RemoveChild(atkData);   // 移除节点
    //添加节点
    XmlElement speed = newxml.CreateElement("speed");
    speed.InnerText = "100";
    Root.AppendChild(speed);
    //修改后要保存   上面只是修改了内存的
    newxml.Save(path);             
}
```

# 4.XML序列化

## 1.序列化与反序列化

序列化:把对象转化为可传输的字节序列过程称为序列化
反序列化:把字节序列还原为对象的过程称为反序列化
说人话:
序列化就是把想要存储的内容转换为字节序列用于存储或传递
反序列化就是把存储或收到的字节序列信息解析读取出来使用

## 2.进行XML序列化

关键知识点
XmlSerializer 用于序列化对象为xml的关键类
StreamWriter 用于存储文件
using 用于方便流对象释放和销毁

```C#
//第一步:确定存储路径
string path = Application.persistentDataPath + "/lesson1.xml";
print(path);
//第二步:结合using 和 StreamWriter 这个流对象 来写入文件
//括号内的代码:写入一个文件流 如果有该文件 直接打开并修改 如果没有该文件 直接新建一个文件
//using 新用法  括号当中包裹的声明的对象 会在 大括号语句块结束后 自动释放掉
//当语句块结束 会自动帮助我们调用 对象的 Dispose这个方法 让其进行销毁
//using一般都是配合 内存占用比较大 或者 有读写操作时 进行使用的
using(StreamWriter stream = new StreamWriter(path))  
{
    //第三步:进行xml文件序列化
    XmlSerializer s = new XmlSerializer(typeof(Lesson1Test));
    s.Serialize(stream,lesson1Test);     //lesson1Test是一个类对象
    //这句代码的含义 就是通过序列化对象,对我们类对象进行翻译 将其翻译成我们的xml文件 写入到对应的文件中
    //第一个参数:文件流对象
    //第二个参数:想要被翻译 的对象
    //注意 :翻译机器的类型一定要和传入的对象是一致的 不然会报错  lesson1Test是Lesson1Test的实例
}
```



# 3.XML反序列化

 关键知识：
 1.using和StreamReader
 2.Xmlserializer 的 Deserialize反序列化方法

````C#
string path = Application.persistentDataPath + "/lesson1.xml";
        
        if(File.Exists(path))   //判断是否存在
        {
            #region 知识点二 反序列化 
            //读取文件
            using (StreamReader sr = new StreamReader(path))
            {
                //XmlSerializer  序列化和反序列化的一个翻译机器
                XmlSerializer s =new XmlSerializer(typeof(Lesson1Test));
                Lesson1Test lesson1 = s.Deserialize(sr) as Lesson1Test;   //注意用于序列化的类不要在申明时赋值 
                                                                          //因为 反序列化的时候再次添加值 可能造成List等数据保存有申明时的数据和反序列化的数据
                print(lesson1);
            }
            #endregion
        }
````

# 4.IXmlSerializable 接口

## 1.IXmlSerializable是什么

C#的XmlSerializer 提供了可拓展内容

可以让一些不能被序列化和反序列化的特殊类能被处理

让特殊类继承 IXmlSerializable 接口 实现其中的方法即可

## 2.实例

```C#
public class TestLesson3:IXmlSerializable
{
    public int test1;
    public string test2;

    //返回结构    
    public XmlSchema GetSchema()
    {
        return null;
    }

    //反序列化时会自动调用的方法
    public void ReadXml(XmlReader reader)
    {
        //里面可以自定义反序列化 的规则
        //读属性
        // test1 = int.Parse(reader.GetAttribute("test1"));
        // test2 = reader.GetAttribute("test2");
        
        //读节点
        //方式一 
        // reader.Read();  //这时读到的是test1节点
        // reader.Read();  //这时读到的是test1节点包裹的内容
        // this.test1 = int.Parse(reader.Value);
        // reader.Read();  //这时读到的是test1尾部包裹节点
        // reader.Read();  //读到test2节点
        // reader.Read();  //读到test2节点包裹的内容
        // this.test2 = reader.Value;
        // //方式二
        // while(reader.Read())
        // {
        //     if(reader.NodeType == XmlNodeType.Element)
        //     {
        //        switch (reader.Name)
        //            {
        //                case "test1":
        //                    reader.Read();
                        //    this.test1 = (int)(reader.ReadElementContentAsString());
        //                    break;
        //                case "test2":
        //                    this.test2 = reader.Value;
        //                    break;
        //            }
        //     }
        // }
        //读包裹节点
        XmlSerializer s = new XmlSerializer(typeof(int));
        XmlSerializer s2 = new XmlSerializer(typeof(string));
        reader.Read();  //跳过根节点
        reader.ReadStartElement("test1");
        test1 = (int)s.Deserialize(reader);
        reader.ReadEndElement();
        reader.ReadStartElement("test2");
        test2 = (string)s2.Deserialize(reader);
        reader.ReadEndElement();
    } 

    //序列化时会自动调用的方法
    public void WriteXml(XmlWriter writer)
    {
        //里面可以自定义序列化 的规则

        //如果要自定义 序列化的规则 一定会用到XmlWriter中的一些方法 来进行序列化
        //写属性
        // writer.WriteAttributeString("test1",test1.ToString());
        // writer.WriteAttributeString("test2",test2);

        //写节点
        // writer.WriteElementString("test1",test1.ToString());
        // writer.WriteElementString("test2",test2);

        //写包裹节点
        XmlSerializer s = new XmlSerializer(typeof(int));
        writer.WriteStartElement("test1");
        s.Serialize(writer,test1);
        writer.WriteEndElement();
        XmlSerializer s2 = new XmlSerializer(typeof(string));
        writer.WriteStartElement("test2");
        s2.Serialize(writer,test2);
        writer.WriteEndElement();
    }

    //在序列化时 引用类型如果为空 不会序列化  xml中看不到该字段
    public override string ToString()
    {
        return $"{test1} {test2}";
    }
}

```

# 5.如何让Dictionary支持xml序列化和反序列化

1.我们没办法修改c#自带的类

2.那我们可以重写一个类 继承Dictionary  然后让这个类继承序列化拓展接口IXmlserializable

3.实现里面的序列化和反序列化方法即可

```C#
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
public class SerializerDictionary<K, V> : Dictionary<K, V>, IXmlSerializable
{
    public XmlSchema GetSchema()
    {
       return null;
    }


    //自定义反序列化规则
    public void ReadXml(XmlReader reader)
    {
        XmlSerializer keyS = new XmlSerializer(typeof(K));
        XmlSerializer valueS = new XmlSerializer(typeof(V));

        reader.Read();   //跳过根节点

        while(reader.NodeType == XmlNodeType.Element)
        {
            K key = (K)keyS.Deserialize(reader);
            V value = (V)valueS.Deserialize(reader);
            //这两个方法会在内部调用 reader.Read() 来逐步解析当前节点的内容，直到完成反序列化为止。
            this.Add(key, value);
        }
    }

    //自定义序列化规则
    public void WriteXml(XmlWriter writer)
    {
        XmlSerializer keyS = new XmlSerializer(typeof(K));
        XmlSerializer valueS = new XmlSerializer(typeof(V));
       foreach(KeyValuePair<K,V> kv in this)
       {
            keyS.Serialize(writer,kv.Key);   //使用流  会自动移动节点
            valueS.Serialize(writer,kv.Value);
       }
    }

    public override string ToString()
    {
        string str = "";
        foreach(KeyValuePair<K,V> kv in this)
        {
            str += kv.Key + ":" + kv.Value + "\n";
        }
        return str;
    }
}
```

# 6.结合之前的可序列化字典进行封装

```C#
public class XMLDataMgr 
{
   private static XMLDataMgr _instance = new XMLDataMgr();
   public static XMLDataMgr Instance => _instance;

   private XMLDataMgr()
   {
   }


    /// <summary>
    /// 保存文件到xml
    /// </summary>
    /// <param name="data">保存的数据</param>
    /// <param name="name">文件名</param>
   public void SaveData(object data,string name)
   {
        //1.得到路径
        string path = Application.persistentDataPath + "/" + name + ".xml";

        //2.存储文件
        using(StreamWriter sw = new StreamWriter(path))
        {
            //3.序列化
            XmlSerializer s = new XmlSerializer(data.GetType());
            s.Serialize(sw,data);
        }
   } 

    public object LoadData(Type type ,string name)
    {
        //1.首先判断文件是否存在
        string path = Application.persistentDataPath + "/" + name + ".xml";
        if(!File.Exists(path))
        path = Application.streamingAssetsPath + "/" + name + ".xml";  //处理不存在文件  换个路径读取
        // if(File.Exists(path))
        {
            //2.读取文件
            using(StreamReader sr = new StreamReader(path))
            {
                //3.反序列化
                XmlSerializer s = new XmlSerializer(type);
                return s.Deserialize(sr);
            }
        }
    }
}
```

