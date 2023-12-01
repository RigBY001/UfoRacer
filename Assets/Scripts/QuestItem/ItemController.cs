using UnityEngine;

public class ItemController : MonoBehaviour{
    public ItemType Item;
    [HideInInspector]
    public MoveItem Move;
    private void Awake() {
        if(TryGetComponent(out MoveItem component)){
            Move = component;
        }
    }
}
