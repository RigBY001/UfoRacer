using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{
    private HashSet<ItemType> _itemInventory;
    public Storage<int> Ore {get; private set;}
   
    private void Awake() {
        Ore = new();
    }
    private void Start() {
        _itemInventory = new()
        {
            ItemType.Auger,
            ItemType.AugerUpdate
        };
    }
    public void ClearInventory() =>_itemInventory.Clear();
    public bool HasItem(ItemType item)=>_itemInventory.Contains(item);
    public void AddItem(ItemType item){
        _itemInventory.Add(item);
    }
    public bool RemoveItem(ItemType item){
        if(_itemInventory.Contains(item)){
            _itemInventory.Remove(item);
            return true;
        }
        return false;
    }
    public void AddOre(int ore){
        Ore.Value += ore;
    }
    public int UnloadOre(){
        int unloadOre = Ore.Value;
        Ore.Value = 0;
        return unloadOre;
    }
    private void OnDestroy() {
        QuestController.OnQuestChainEnd-=ClearInventory;
    }
}
