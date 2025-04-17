using System.Collections.Generic;

public class Item
{
    public int id;
    public int num;
    public Item(int id , int num)
    {
        this.id = id;
        this.num = num;
    }
}
public class PlayerInfo
{
    public string name;
    public int atk;
    public int def;
    public float moveSpeed;
    public float rotateSpeed;
    public Item weapon;
    public List<int> ListInt;
    public List<Item> ItemList;
    public Dictionary<int,Item> ItemDict;
    public PlayerInfo(string name,int atk,int def,float moveSpeed,float rotateSpeed,Item weapon,List<int> ListInt,List<Item> ItemList,Dictionary<int,Item> ItemDict) 
    {
        this.name = name;
        this.atk = atk;
        this.def = def;
        this.moveSpeed = moveSpeed;
        this.rotateSpeed = rotateSpeed;
        this.weapon = weapon;
        this.ListInt = ListInt;
        this.ItemList = ItemList;
        this.ItemDict = ItemDict;
        System.Console.WriteLine("加载结束");
    }

    public override string ToString()
    {
        string str = "name = " + name + "\n";
        str += "atk = " + atk + "\n";
        str += "def = " + def + "\n";
        str += "moveSpeed = " + moveSpeed + "\n";
        str += "rotateSpeed = " + rotateSpeed + "\n";
        str += "weapon = " + weapon + "\n";
        int i = 1;
        foreach (int item in ListInt)
        {
            str += "ListInt = "+ i + item + "\n";
            i++;
        }
        i = 1;
        foreach (Item item in ItemList)
        {
            str += "ItemList = "+ i + item.id + "num:"+ item.id+"\n";
        }
        foreach (KeyValuePair<int,Item> item in ItemDict)
        {
            str += "ItemDict = "+ item.Key + item.Value.id + "num:"+ item.Value.id+"\n";
        }
        return str;
    }
}