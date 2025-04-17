using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Lesson2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //C#读取XML的方法有几种
        //1.XmlDocument  (把数据加载到内存中，方便读取)
        //2.XmlTextReader(以流形式加载，内存占用更少，但是是单向只读，使用不是特别方便，除非有特殊需求，否则不会使用)
        //3.Linq

        //使用XmlDocument类读取是较方便 最容易理解和操作的方法

        #region 知识点一 读取XML文件信息
        XmlDocument xml = new XmlDocument();
        //通过XmlDocument读取xml文件 有两个API
        //1.直接根据xml字符串内容 来加载xml文件
        //存放在Resorces文件夹下的xml文件加载处理
        TextAsset textAsset =  Resources.Load<TextAsset>("Test");
        print(textAsset.text);
        //通过这个方法 就能够翻译字符串为xml对象
        xml.LoadXml(textAsset.text);

        //通过xml文件的路径去进行加载
        // xml.Load(Application.streamingAssetsPath + "/Test.xml");
        #endregion

        #region 知识点二 读取元素和属性信息
        // 节点信息类
        // XmlNode 单个节点信息类
        // 节点列表信息
        // XmlNodeList   多个节点信息类

        //获取xml中的根节点
        XmlNode root = xml.SelectSingleNode("Root");

        //再通过根节点 去获取下面的子节点
        XmlNode nodeName = root.SelectSingleNode("name");
        //如果想要获取节点包裹的元素 直接 .InnerText
        print(nodeName.InnerText);
        
        //属性的获取
        XmlNode nodeItem = root.SelectSingleNode("Item");
        //第一种 通过 中括号获取信息
        print(nodeItem.Attributes["id"].Value);
        print(nodeItem.Attributes["num"].Value);
        //第二种 通过属性名称获取信息
        print(nodeItem.Attributes.GetNamedItem("id").Value);

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
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
