using HFSM;
using TDSTK_UI;
using UnityEngine.SceneManagement;

namespace GarageState{
public class PlayerEnter : State{
    private Garage _garage;

    public PlayerEnter(Garage garage){
        _garage = garage;
        
    }
    protected override void OnEnter(){
        // if(_garage.Player.TryGetComponent(out Inventory inventory)) WorkWithInventory(inventory);
        // if(!UIWeaponAbilityTab.IsOn()) UIWeaponAbilityTab.TurnTabOn();
        Garage.OnEnterGarge?.Invoke();
        SceneManager.LoadScene("Garage",LoadSceneMode.Single);
    }
    private void WorkWithInventory(Inventory inventory){
        _garage.OreStorege.Value += inventory.UnloadOre();
        inventory.ClearInventory();
    }
}
}
