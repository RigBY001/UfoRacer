using System;
using UnityEngine;
using HFSM;
using ZoneState;
using HUDIndicator;

public class MiningZone : MonoBehaviour{
    [SerializeField] private PlayerScaner _playerScaner;
    [SerializeField] private GameObject _zoneOfOre;
    public GameObject ZoneOfOre=>_zoneOfOre;
    [SerializeField] private Auger _auger; 
    public Auger Auger=>_auger;
    [SerializeField] IndicatorBarOnScreen _progerssBar;
    public IndicatorBarOnScreen ProgressBar=>_progerssBar;

    public GameObject Player {get;private set;}

    private MiningZoneStateMachine _mainStateMachine;
 
    private void Start() {
        _auger.Init();
        Empty empty = new(this);
        Builiding building = new(this);
        Installed installed = new(this);

        _mainStateMachine = new (empty,building,installed,_auger.StateMachine);
        _mainStateMachine.Init();

        empty.AddTransition(building,CheckPlayerInventory);
        building.AddEventTransition(empty,()=>Player == null);
        building.OnBuilded += building.AddEventTransition(installed);
        installed.OnInstaled += installed.AddEventTransition(_auger.StateMachine);
       _auger.OnCrash += _auger.StateMachine.AddEventTransition(empty);

       _playerScaner.OnPlayerEnter +=OnPlayerEnter;
       _playerScaner.OnPlayerExit  +=OnPlayerExit;
    }

    private void Update() {
        _mainStateMachine.Update();
    }
    private bool CheckPlayerInventory(){
        if(Player == null) return false;
        if(Player.TryGetComponent(out Inventory inventory)){
        return inventory.HasItem(ItemType.Auger);
        }
        return false;
    }
    private void OnPlayerEnter(){
        Player = _playerScaner.Player;
    }
    private void OnPlayerExit(){
        Player = null;
    }
}
