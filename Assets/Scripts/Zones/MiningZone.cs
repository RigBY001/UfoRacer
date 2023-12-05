using System;
using UnityEngine;
using HFSM;


public class MiningZone : MonoBehaviour{
    [SerializeField] private GameObject _zoneOfOre;
    public GameObject ZoneOfOre=>_zoneOfOre;
    [SerializeField] private GameObject _auger; 
    public GameObject Auger=>_auger;
    [SerializeField] private GameObject _boxOfOre;    
    public GameObject BoxOfOre=>_boxOfOre;
    [SerializeField] private GameObject _augerBrokenIndicator;    
    public GameObject AugerBrokenIndicator=>_augerBrokenIndicator;
    public GameObject Player {get;private set;}

    private MiningZoneStateMachine _mainStateMachine;
    public Action OnPlayerEnter;

    private void Start() {
        NoneAuger noneAuger = new(this);
        Initial initial = new(this);
        AugerNormal augerNormal = new(this);
        FullOfOre fullOfOre = new(this);
        Empty empty = new(this);
        AugerBroken augerBroken = new(this);
        AugerCrash augerCrash = new(this);

        _mainStateMachine = new (noneAuger,initial,augerNormal,fullOfOre,empty,
        augerBroken,augerCrash);
        _mainStateMachine.Init();

        OnPlayerEnter += noneAuger.AddEventTransition(initial,CheckPlayerInventory);
        OnPlayerEnter += fullOfOre.AddEventTransition(empty);
        initial.AddTransition(augerNormal,()=>initial.IsActive);
        Garage.OnEnterGarge+= augerNormal.AddEventTransition(fullOfOre);
        Garage.OnEnterGarge+= fullOfOre.AddEventTransition(augerBroken);
        Garage.OnEnterGarge+= empty.AddEventTransition(augerBroken);
        Garage.OnEnterGarge+= augerBroken.AddEventTransition(augerCrash);
        OnPlayerEnter+= augerBroken.AddEventTransition(augerNormal);
        augerCrash.AddTransition(noneAuger,()=>augerCrash.IsActive);
    }

    private void Update() {
        _mainStateMachine.Update();
    }
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")) return;
        Player = other.gameObject;
        OnPlayerEnter?.Invoke();
    }
    private bool CheckPlayerInventory(){
        Player.TryGetComponent(out Inventory inventory);
        if(inventory.RemoveItem(ItemType.Auger)) return true;
        return false;
    }
}
