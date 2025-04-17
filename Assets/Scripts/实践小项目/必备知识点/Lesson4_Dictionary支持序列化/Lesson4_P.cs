using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class TestLesson4
{
    public int test1 = 1;
    public string test2 = "test2";
    public SerializerDictionary<string, int> test3;
    public int test4 = 4;

    public override string ToString()
    {
        return "test1:"+test1+" test2:"+test2+" test3:"+test3+" test4:"+test4;
    }
}
public class Lesson4_P : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 如何让Dictionary支持xml序列化和反序列化
        // 1.我们没办法修改c#自带的类
        // 2.那我们可以重写一个类 继承Dictionary  然后让这个类继承序列化拓展接口IXmlserializable
        // 3.实现里面的序列化和反序列化方法即可
        #endregion   

        #region 让Dictionary支持序列化和反序列化
        string path = Application.persistentDataPath + "/testDictionary.xml";
        TestLesson4 test = new TestLesson4();
        test.test3 = new SerializerDictionary<string, int>();
        test.test3.Add("test1",1);
        test.test3.Add("test2",2);
        using(StreamWriter sw = new StreamWriter(path))
        {
            XmlSerializer xml = new XmlSerializer(typeof(TestLesson4));
            xml.Serialize(sw,test);
        }

        using(StreamReader sr = new StreamReader(path))
        {
            XmlSerializer xml = new XmlSerializer(typeof(TestLesson4));
            TestLesson4  tl =  xml.Deserialize(sr) as TestLesson4;
            print(tl);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
