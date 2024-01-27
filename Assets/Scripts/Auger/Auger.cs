using System;
using UnityEngine;
using AugerState;
using HUDIndicator;

public class Auger : MonoBehaviour{
    [SerializeField] IndicatorBarOnScreen _progerssBar;
    public IndicatorBarOnScreen ProgressBar=>_progerssBar;
    [SerializeField] private Scaner _playerScaner;
    [SerializeField] private GameObject _model;
    //[SerializeField] private GameObject _roundZone;
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
  
    public void Awake() {
        _currentAmountOfOre = 0;
        Normal normal = new(this);
        FullOfOre fullOfOre = new(this);
        ExtractOre extractOre = new(this);
        Empty empty = new(this);
        Broken broken = new(this);
        Crash augerCrash = new(this);
        Repair repair = new(this);
        Update update = new (this);
    
        StateMachine = new ( normal,update,fullOfOre,extractOre,empty,broken,repair,augerCrash);

        
        fullOfOre.AddTransition(extractOre,()=> Player != null);
        broken.AddTransition(repair,()=>Player != null);
        repair.AddTransition(broken,()=>Player == null);
        update.AddTransition(normal,()=>Player == null);
        normal.AddTransition(update,CheckPlayerInventory);
        normal.OnFulled+= normal.AddEventTransition(fullOfOre);
        empty.OnEmpted += empty.AddEventTransition(normal);
        // Garage.OnEnterGarge+= augerNormal.AddEventTransition(fullOfOre);
        // Garage.OnEnterGarge+= fullOfOre.AddEventTransition(augerBroken);
        // Garage.OnEnterGarge+= empty.AddEventTransition(augerBroken);
        // Garage.OnEnterGarge+= extractOre.AddEventTransition(augerBroken); 
        // Garage.OnEnterGarge+= augerBroken.AddEventTransition(augerCrash);

        extractOre.OnDoneUnloading += extractOre.AddEventTransition(empty);
        repair.OnRepair += repair.AddEventTransition(normal);
        update.OnUpdated += update.AddEventTransition(normal);
        
        update.OnUpdated += UpdateAuger;
    }
    private void Start(){
        //_roundZone.SetActive(false);
        _playerScaner.OnEnter += OnPlayerEnter;
        _playerScaner.OnExit  += OnPlayerExit;
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
        //_roundZone.SetActive(true);
    }
    private void OnPlayerExit(){
        Player = null;
        //_roundZone.SetActive(false);
    }
}