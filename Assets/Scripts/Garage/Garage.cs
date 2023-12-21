using System;
using UnityEngine;
using TDSTK;
using TDSTK_UI;
using MoreMountains.HighroadEngine;

public class Garage : MonoBehaviour{
    [SerializeField] private UnitPlayer _unitPlayer1;
    [SerializeField] private UnitPlayer _unitPlayer2;
    [SerializeField] private Monitor _oreMonitor;
    public static Action OnEnterGarge,OnExitGarage;
    public Storage<int> OreStorege{get;private set;}
    public Storage<int> GoldStorege{get;private set;}
    public bool IsQuestChainEnd{get;private set;}
    
    public static Garage Instance;

    private void Awake() {
        IsQuestChainEnd = false;
        OreStorege = new();
        GoldStorege = new();
        Instance = this;
    }
    private void Start() {
        _unitPlayer1.gameObject.GetComponent<SolidController>().EnableControls(1);
        GameControl.SetPlayer(_unitPlayer1);
        _oreMonitor.Init(OreStorege);
    }
    private void OnQuestChainEnd(){
        IsQuestChainEnd = true;
    }
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")) return;
        if(other.TryGetComponent(out Inventory inventory)) WorkWithInventory(inventory);
        if(!UIWeaponAbilityTab.IsOn()) UIWeaponAbilityTab.TurnTabOn();
        OnEnterGarge?.Invoke();
    }  
    private void WorkWithInventory(Inventory inventory){
        OreStorege.Value += inventory.UnloadOre();
        inventory.ClearInventory();
    }
    private void OnTriggerExit(Collider other) {
        OnExitGarage?.Invoke();
    }
    public void ChangePlayer(){
        UnitPlayer currentPlayer = GameControl.GetPlayer();
        if(currentPlayer != _unitPlayer1) {
            GameControl.SetPlayer(_unitPlayer1);
            var controller =  _unitPlayer2.gameObject.GetComponent<SolidController>();
            controller.DisableControls();
            controller =  _unitPlayer1.gameObject.GetComponent<SolidController>();
            controller.EnableControls(1);
            Debug.Log(GameControl.GetPlayer().name);
            return;
        }
        if(currentPlayer != _unitPlayer2) {
            GameControl.SetPlayer(_unitPlayer2);
            var controller =  _unitPlayer1.gameObject.GetComponent<SolidController>();
            controller.DisableControls();
            controller =  _unitPlayer2.gameObject.GetComponent<SolidController>();
            controller.EnableControls(2);
            Debug.Log(GameControl.GetPlayer().name);
            return;
        }
    }
}
