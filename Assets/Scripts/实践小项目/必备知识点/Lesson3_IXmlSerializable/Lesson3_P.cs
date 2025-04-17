using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

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

public class Lesson3_P : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 IXmlSerializable是什么
        // C#的XmlSerializer 提供了可拓展内容
        // 可以让一些不能被序列化和反序列化的特殊类能被处理
        // 让特殊类继承 IXmlSerializable 接口 实现其中的方法即可
        #endregion

        #region 知识点二 自定义类的实践
        TestLesson3 t = new TestLesson3();
        t.test2 = "test2";
        string path = Application.persistentDataPath + "/TestLesson3.xml";
        //序列化
        using(StreamWriter writer = new StreamWriter(path))
        {
            XmlSerializer s = new XmlSerializer(typeof(TestLesson3));
            s.Serialize(writer,t);
        }

        //反序列化
        using(StreamReader reader = new StreamReader(path))
        {
            XmlSerializer s = new XmlSerializer(typeof(TestLesson3));
            TestLesson3 t2 = s.Deserialize(reader) as TestLesson3;
            print(t2);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

