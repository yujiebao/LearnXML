using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    PlayerInfo playerInfo;
    // Start is called before the first frame update
    void Start()
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(Resources.Load<TextAsset>("PlayerInfo").text);
        XmlNode root = xml.SelectSingleNode("PlayerInfo");
        XmlNode name = root.SelectSingleNode("name");
        XmlNode atk = root.SelectSingleNode("atk");
        int atkdate = Convert.ToInt32(atk.InnerText);
        XmlNode def = root.SelectSingleNode("def");
        int defdate = Convert.ToInt32(def.InnerText);
        XmlNode moveSpeed = root.SelectSingleNode("moveSpeed");
        float movespeeddate = Convert.ToSingle(moveSpeed.InnerText);
        XmlNode rotateSpeed = root.SelectSingleNode("rotateSpeed");
        float rotatespeeddate = Convert.ToSingle(rotateSpeed.InnerText);
        XmlNode weapon = root.SelectSingleNode("Item");
        
        Item item = new Item(Convert.ToInt32(weapon.SelectSingleNode("id").InnerText),Convert.ToInt32(weapon.SelectSingleNode("num").InnerText));
        XmlNode ListInt = root.SelectSingleNode("ListInt");
        List<int> listint = new List<int>();
        for(int i = 0; i < ListInt.Attributes.Count; i++)
        {
            listint.Add(Convert.ToInt32(ListInt.Attributes[i].Value));
            // 读取属性使用Value和InnerText都可以
            // print("Value:  " + Convert.ToInt32(ListInt.Attributes[i].Value));
            // print("InnerText:  " + Convert.ToInt32(ListInt.Attributes[i].InnerText));

        }
        XmlNode ItemList = root.SelectSingleNode("ItemList");
        XmlNodeList xmlNodeList = ItemList.SelectNodes("Item");
        List<Item> itemlist = new List<Item>(); 
        Item itemdate;
        foreach(XmlNode tmp in xmlNodeList)
        {   
            itemdate = new Item(Convert.ToInt32(tmp.SelectSingleNode("id").InnerText),Convert.ToInt32(tmp.SelectSingleNode("num").InnerText));
            //读取子节点 使用InnerText都可以  使用Value会出错
            // print("Value:  " + Convert.ToInt32(tmp.SelectSingleNode("id").Value));
            // print("InnerText:  " + Convert.ToInt32(tmp.SelectSingleNode("id").InnerText));
            itemlist.Add(itemdate);
        }
        XmlNode ItemDict = root.SelectSingleNode("ItemDic");
        XmlNodeList DicList = ItemDict.SelectNodes("data");
        Dictionary<int,Item> itemdict = new Dictionary<int,Item>();
        int id;
        XmlNode xmlNode;
        foreach(XmlNode tmp in DicList)
        {    
            id = Convert.ToInt32(tmp.SelectSingleNode("id").InnerText);
            xmlNode = tmp.SelectSingleNode("Item");
            itemdate = new Item(Convert.ToInt32(xmlNode.SelectSingleNode("id").InnerText),Convert.ToInt32(xmlNode.SelectSingleNode("num").InnerText));
            itemdict.Add(id,itemdate);
        }

        playerInfo = new PlayerInfo(name.InnerText,atkdate,defdate,movespeeddate,rotatespeeddate,item,listint,itemlist,itemdict);
        print(playerInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
