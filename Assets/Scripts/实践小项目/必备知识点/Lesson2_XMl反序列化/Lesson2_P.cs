using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Lesson2_P : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 判断文件是否存在
        string path = Application.persistentDataPath + "/lesson1.xml";
        
        if(File.Exists(path))   //判断是否存在
        {
            #region 知识点二 反序列化  
            //关键知识!=
            //1.using和StreamReader
            //2.Xmlserializer 的 Deserialize反序列化方法

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

        #endregion

        #region 总结
        //1.判断文件是否存在 File.Exists
        //2,女件流获取StreamReader reader = new StreamReader(path)
        //3.根据文件流 XmlSerializer通过Deserialize反序列化 出对象
        
        //注意:List对象 如果有默认值 反序列化时 不会清空 会往后面添加
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
