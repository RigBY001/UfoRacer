using UnityEngine.Search;
using UnityEngine;

public class TakeItem : QuestConnector{
    [SearchContext("t:ItemController ", SearchViewFlags.GridView,"prefab")]
    public ItemController _itemController;   
    [SerializeField] private float _distance;
 

    private void Update() {
        if(_itemController.Move.IsEnd) {
            Zone.EndQuest();
            _itemController.gameObject.SetActive(false);
        }
    }
   
    private void OnTriggerEnter(Collider other) {
        Inventory inventory =  other.GetComponent<Inventory>();
        if(inventory == null) return;
        inventory.AddItem(_itemController.Item);
        _itemController.Move.GoUpFromParent(transform,_distance);
        _indicator.SetActive(false);
    }
}
