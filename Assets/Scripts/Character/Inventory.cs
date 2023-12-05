using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{
    private HashSet<ItemType> _itemInventory;
    private void Start() {
        _itemInventory = new();
        _itemInventory.Add(ItemType.Auger);
    }
    public void ClearInventory() =>_itemInventory.Clear();
    
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
    private void OnDestroy() {
        QuestController.OnQuestChainEnd-=ClearInventory;
    }
}
