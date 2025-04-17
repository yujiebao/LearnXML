using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 决定存储在哪个文件夹下
        //注意:
        // 1.Resources 可读 不可写 打包后找不到
        // 2.Application.streamingAssetsPath 可读 PC端可写 找得到   IOS和Android不可写
        // 3.Application.dataPath 打包后找不到
        // 4.Application.persisterkDataPath 可读可写找得到
        string path = Application.persistentDataPath + "/PlayerInfo2.xml";
        print(Application.persistentDataPath);
        #endregion

        #region 知识点二 存储xml文件
        //关键类 XmlDecument 用于创建节点 存储文件
        //关键类 XmlDeclaration 用于添加版本信息
        //关键类 XmlElement 节点类

        //存储有五步：
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
        #endregion  
    
        #region 知识点三 修改Xml文件
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
        #endregion      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
