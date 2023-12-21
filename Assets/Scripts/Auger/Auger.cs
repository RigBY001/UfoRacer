using System;
using UnityEngine;
using AugerState;
using HUDIndicator;

public class Auger : MonoBehaviour{
    [SerializeField] IndicatorBarOnScreen _progerssBar;
    public IndicatorBarOnScreen ProgressBar=>_progerssBar;
    [SerializeField] private Scaner _playerScaner;
    [SerializeField] private GameObject _model;
    [SerializeField] private int _maxAmountOfOre;
    public int MaxAmountOfOre =>_maxAmountOfOre;
    [SerializeField] private int _unloadingOreCount;
    [SerializeField] private GameObject _boxOfOre;    
    public GameObject BoxOfOre=>_boxOfOre;
    [SerializeField] private GameObject _augerBrokenIndicator;    
    public GameObject AugerBrokenIndicator=>_augerBrokenIndicator;
    private int _currentAmountOfOre;
    public bool IsEmpty=>_currentAmountOfOre == 0;

    public Action OnCrash;
    public AugerStateMachine StateMachine{get;private set;}
    
    public GameObject Player{get;private set;}
  
    public void Init() {
        _currentAmountOfOre = 0;
        Normal augerNormal = new(this);
        FullOfOre fullOfOre = new(this);
        ExtractOre extractOre = new(this);
        Empty empty = new(this);
        Broken augerBroken = new(this);
        Crash augerCrash = new(this);
        Repair repairAuger = new(this);
        Update updateAuger = new (this);
    
        StateMachine = new ( augerNormal,updateAuger,fullOfOre,extractOre,empty,augerBroken,repairAuger,augerCrash);

        
        fullOfOre.AddTransition(extractOre,()=> Player != null);
        augerBroken.AddTransition(repairAuger,()=>Player != null);
        repairAuger.AddTransition(augerBroken,()=>Player == null);
        augerNormal.AddTransition(updateAuger,CheckPlayerInventory);

        Garage.OnEnterGarge+= augerNormal.AddEventTransition(fullOfOre);
        Garage.OnEnterGarge+= fullOfOre.AddEventTransition(augerBroken);
        Garage.OnEnterGarge+= empty.AddEventTransition(augerBroken);
        Garage.OnEnterGarge+= extractOre.AddEventTransition(augerBroken); 
        Garage.OnEnterGarge+= augerBroken.AddEventTransition(augerCrash);

        extractOre.OnDoneUnloading += extractOre.AddEventTransition(empty);
        repairAuger.OnRepair += repairAuger.AddEventTransition(augerNormal);
        updateAuger.OnUpdated += updateAuger.AddEventTransition(augerNormal);

        _playerScaner.OnPlayerEnter += OnPlayerEnter;
        _playerScaner.OnPlayerExit  += OnPlayerExit;
        updateAuger.OnUpdated += UpdateAuger;
    }
    public void Eneble(){
        _model.SetActive(true);
    }
    public void Disable(){
        _model.SetActive(false);
    }
    public void MiningOre(){
        _currentAmountOfOre = _maxAmountOfOre;
    }
    public int UnloadingOre(){
        if(IsEmpty){
            return 0;
        }
        _currentAmountOfOre-=_unloadingOreCount;
        if(_currentAmountOfOre <0) {
            int _remain = _unloadingOreCount - _currentAmountOfOre;
            _currentAmountOfOre =0;
            return _remain;
        }
        return _unloadingOreCount;
    }
    private bool CheckPlayerInventory(){
        if(Player == null) return false;
        if(Player.TryGetComponent(out Inventory inventory)){
        return inventory.HasItem(ItemType.AugerUpdate);
        }
        return false;
    }
    private void UpdateAuger(){
        if(Player == null) return;
        if(Player.TryGetComponent(out Inventory inventory))
        if(inventory == null) return;
        inventory.RemoveItem(ItemType.AugerUpdate);
        _maxAmountOfOre *= 2;
        _unloadingOreCount *= 2;
    }
    private void OnPlayerEnter(){
        Player = _playerScaner.ScanedObject;
    }
    private void OnPlayerExit(){
        Player = null;
    }
}