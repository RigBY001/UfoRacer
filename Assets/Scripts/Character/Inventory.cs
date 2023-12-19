using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{
    private HashSet<ItemType> _itemInventory;
    public int Ore {get;private set;}
    public Action OnChangeOre;
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
        Ore += ore;
        OnChangeOre?.Invoke();
    }
    private void OnDestroy() {
        QuestController.OnQuestChainEnd-=ClearInventory;
    }
}
