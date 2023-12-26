using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreExchanger : MonoBehaviour{
    [SerializeField] private Scaner _playerScaner;
    [SerializeField] private Shop _shop;

    private void Start() {
        _playerScaner.OnEnter += OnPlayerEnter;
        _playerScaner.OnExit += OnPlayerExit;
    }
    private void OnPlayerEnter(){
        Inventory inventory = _playerScaner.ScanedObject.GetComponent<Inventory>();
        _shop.AddGold(inventory.UnloadOre());
    }
    private void OnPlayerExit(){
        
    }
}
