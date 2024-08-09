using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory<T>:List<T> where T:Containable{
    
    protected int capacity;
    public int statIndex;
    public Inventory(){}
    public Inventory(int capacity):base(capacity){
        this.capacity = capacity;
    }
    public Inventory(int capacity, int persistenceIndex):base(capacity){
        this.capacity = capacity;
        this.statIndex = persistenceIndex;
    }
    public int GetCapacity(Inventory<T> list){return list.GetCapacity();}
    public int GetCapacity(){return capacity;}
    public bool AddItem(T item){
        if(CountItems() == GetCapacity())
            return false;
        Add(item);
        return true;
    }
    public bool AddItem(T item, Inventory<T> inventory){
        if(CountItems() == GetCapacity(inventory))
            return false;
        Add(item);
        return true;
    }
    public bool InsertItem(T item,int index){
        if(CountItems() == capacity)
            return false;
        Insert(index, item);
        return true;
    }
    public (T,int) RemoveItem(int index){
        (T,int) temp = (this[index],index);
        RemoveAt(index);
        return temp;
    }
    public (T,int) RemoveItem(T item){
        var index = GetItemIndex(item);
        (T,int) temp = (null,-1);
        if(index>=0){
         temp = (this[index],index);
            RemoveAt(index);}
        return temp;
    }
    public int GetItemIndex(T item){
        //find first match
        return FindIndex(0,1,item.Equals);
    }
    private int CountItems(){
        var count = 0;
        foreach(T i in this){
            if(!i.GetName().Equals("") && count<capacity)
                count++;
        }
        return count;
    }
    public void SetPersistentStats(){
        Dictionary<Stat,int> stats = new Dictionary<Stat, int>();
        foreach(T item in this){
            if(item.GetEquipped()){
                var temp = item.GetStats();
                foreach(Stat stat in temp.Keys){
                    if(stats.ContainsKey(stat))
                        stats[stat] = stats[stat]+temp[stat];        
                    else{stats.Add(stat,temp[stat]);}
                }       
            }
        }
        Stats.inventories[statIndex] = stats;
    }
    public Dictionary<Stat,int> GetStatsNonPersistent(){
        Dictionary<Stat,int> stats = new Dictionary<Stat, int>();
        foreach(T item in this){
            if(item.GetEquipped()){
                var temp = item.GetStats();
                foreach(var(key,value) in temp){
                    if(stats.ContainsKey(key))
                        stats[key] = stats[key]+value;        
                    else{stats.Add(key,value);}
                }       
            }
        }
        return stats;
    }
}
