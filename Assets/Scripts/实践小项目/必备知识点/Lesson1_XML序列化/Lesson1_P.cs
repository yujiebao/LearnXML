using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


public class Lesson1Test
{
    //使用序列化和反序列化的类不建议在声明时赋初值
    [XmlAttribute]
    public int testpublic = 1;  
    
    private int testprivate = 2;
    protected int testprotected = 3;
    internal int testinternal = 4;

    [XmlElement("Tesssdsads")]
    public string teststring = "teststring";

    public int intPro{get;set;}
    public Lesson1Test2 testclass = new Lesson1Test2();
    //支持public 的list '
    [XmlArray("ArrayInt")]
    [XmlArrayItem("Int")]
    public List<int> listInt = new List<int>(){1,2,3,4,5};
    public int[] arrayInt = new int[]{1,2,3,4,5};
    public List<Lesson1Test2> listClass = new List<Lesson1Test2>(){new Lesson1Test2(),new Lesson1Test2()};
    //但是不支持 Dictionary

    public override string ToString(){
            return $"testpublic: {testpublic}, " +
               $"testprivate: {testprivate}, " +
               $"testprotected: {testprotected}, " +
               $"testinternal: {testinternal}, " +
               $"teststring: {teststring}, " +
               $"intPro: {intPro}, " +
               $"testclass: {testclass}, " +
               $"listInt: [{string.Join(", ", listInt)}], " +
               $"arrayInt: [{string.Join(", ", arrayInt)}], " +
               $"listClass: [{string.Join(", ", listClass)}]";
    }
}

public class Lesson1Test2
{
    [XmlAttribute("Test")]
    public int test1 = 1;
    public float test2 = 2.5f;
    public bool test3 = true;
}
public class Lesson1_P : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是序列化和反序列化
        //序列化:把对象转化为可传输的字节序列过程称为序列化
        //反序列化:把字节序列还原为对象的过程称为反序列化
        //说人话:
        //序列化就是把想要存储的内容转换为字节序列用于存储或传递
        //反序列化就是把存储或收到的字节序列信息解析读取出来使用
        #endregion

        #region 知识点二 XML序列化
        //1.第一步准备一个数据结构类
        Lesson1Test lesson1Test = new Lesson1Test();
        
        //2.进行序列化
        //关键知识点
        //XmlSerializer 用于序列化对象为xml的关键类
        //StreamWriter 用于存储文件
        //using 用于方便流对象释放和销毁

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
            s.Serialize(stream,lesson1Test);   //这句代码的含义 就是通过序列化对象,对我们类对象进行翻译 将其翻译成我们的xml文件 写入到对应的文件中
            //第一个参数:文件流对象
            //第二个参数:想要被翻译 的对象
            //注意 :翻译机器的类型一定要和传入的对象是一致的 不然会报错  lesson1Test是Lesson1Test的实例
        }
        #endregion

        #region 知识点三 自定义节点名 或 设置属性
        
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
