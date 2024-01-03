using UnityEngine;

public class SellArea : MonoBehaviour{
    [SerializeField] private QuestController _questController;
    [SerializeField] private Transform _spawnPoint;
    public void StartArea(){
        _questController.StartQuest();
        Transform playerTransform = PlayersManager.Instance.CurrentPlayer.GameObject.transform;
        playerTransform.position = _spawnPoint.position;
        playerTransform.rotation = Quaternion.Euler(0,90,0);
    }
    public void EndArea(){
        _questController.DisableQuestController();
        Inventory mazInventory = PlayersManager.Instance.CurrentPlayer.Inventroy;   
        Garage.Instance.OreStorage.Value += mazInventory.UnloadOre();
        Shop.Instance.AddGold( mazInventory.UnloadGold());  
        PlayersManager.Instance.SetPlayer1();

    }
}
