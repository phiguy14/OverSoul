using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containable{
    protected string name;
    protected string description;
    protected int value;
    private int timeToLive;
    private bool isEquipped;
    public int itemCount;
    protected Dictionary<Stat,int> stats;
    public ItemClass itemClass;
    
    public Containable(){
        this.name = "";
        this.description = "";
        this.value = 0;
        this.stats = null;
        this.isEquipped = false;
        this.itemCount=0;
    }
    public Containable(string name, string description, int value, Dictionary<Stat,int> stats){
        this.name = name;
        this.description = description;
        this.value = value;
        this.stats = stats;
        this.isEquipped = false;
        this.itemCount=1;
    }
    public Containable(string name, string description, int value, Dictionary<Stat,int> stats, int timeToLive){
        this.name = name;
        this.description = description;
        this.value = value;
        this.stats = stats;
        this.timeToLive = timeToLive;
        this.isEquipped = true;
        this.itemCount=1;
    }
    public void AddStat(Stat type, int value){
        if(stats == null)
            stats = new Dictionary<Stat,int>();
        if(stats.ContainsKey(type))
            stats[type] = stats[type]+value;        
        else{stats.Add(type,value);}
    }
    public Containable(string name, string description, int value,int itemCount, Dictionary<Stat,int> stats){
        this.name = name;
        this.description = description;
        this.value = value;
        this.stats = stats;
        this.isEquipped = true;
        this.itemCount=itemCount;
    }
    public int GetStat(Stat type){return stats[type];}
    public Dictionary<Stat,int> GetStats(){return stats;}
    public string GetName(){return name;}
    public int GetTimeToLive(){return timeToLive;}
    public void RemoveStat(Stat type){stats.Remove(type);}
    public bool Equals(Ability item){
        return this.name.Equals(item.name)
        && this.description.Equals(item.description);
        //&& StatsEqual(item.stats,this.stats);
    }
    protected bool StatsEqual(Dictionary<Stat,int>  stats1, Dictionary<Stat,int>  stats2){
        if(stats1.Count!=stats2.Count)
            return false;
        foreach(Stat stat in stats1.Keys){
            if(stats1[stat] != stats2[stat])
                return false;
        }
        return true;
    }
    public void ChangeEquipped(){
        this.isEquipped = !this.isEquipped;
        Stats.RefreshPlayerStats();
    }
    public bool GetEquipped(){return isEquipped;}
}

