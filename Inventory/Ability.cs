using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability:Containable{
    public Ability():base(){}
    public Ability(string name, string description, int value, Dictionary<Stat,int> stats)
        :base(name,description,value,stats){}
    public Ability(string name, string description, int value, Dictionary<Stat,int> stats, int timeToLive)
        :base(name,description,value,stats,timeToLive){this.itemClass=ItemClass.Headwear;}
}