using System.Collections.Generic;
public enum ItemClass{
    Headwear=(0),
    Armor=(1),
    Weapon=(2),
    Accessory=(3),
    Consumable=(4),
    Other=(5)
}
public class Item:Containable{   
    public Item():base(){}
    public Item(string name, string description, int value, Dictionary<Stat,int> stats)
        :base(name,description,value,stats){this.itemClass=ItemClass.Headwear;}
    public Item(string name, string description, int value, Dictionary<Stat,int> stats,ItemClass itemClass)
        :base(name,description,value,stats){this.itemClass=itemClass;}  
}
