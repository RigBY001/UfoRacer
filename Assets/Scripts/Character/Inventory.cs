using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{
    private HashSet<ItemType> _itemInventory;
    public IntStorage Ore {get; private set;}
    public IntStorage Gold {get; private set;}
    
    private void Awake() {
        Ore = new(int.MaxValue);
        Gold = new(int.MaxValue);
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
    public void AddGold(int ore){
        Gold.Value += ore;
    }
    public int UnloadOre(){
        int unloadOre = Ore.Value;
        Ore.Value = 0;
        return unloadOre;
    }
    public int UnloadGold(){
        int UnloadGold = Gold.Value;
        Gold.Value = 0;
        return UnloadGold;
    }
    private void OnDestroy() {
        QuestController.OnQuestChainEnd-=ClearInventory;
    }
}
