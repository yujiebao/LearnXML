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