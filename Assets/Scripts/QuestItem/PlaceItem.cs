using UnityEngine.Search;
using UnityEngine;

public class PlaceItem : QuestConnector{
    [SearchContext("t:ItemController ", SearchViewFlags.GridView,"prefab")]
    public ItemController _itemController;
    private ItemController _tempController;   
    [SerializeField] private float _distance;

    private void Update() {
        if(_tempController == null) return;
        if(_tempController.Move.IsEnd) {
            Zone.EndQuest();
            Destroy(_tempController);
        }
    }
 
    private void OnTriggerEnter(Collider other) {
        Inventory inventory =  other.GetComponent<Inventory>();
        if(inventory == null) return;
        bool hasItem =  inventory.RemoveItem(_itemController.Item);
        if(hasItem == false) return;
        _tempController = Instantiate(_itemController);
        _tempController.Move.GoDownToParent(transform,_distance);
        _indicator.SetActive(false);
    }
}
