using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;
using TDSTK;
using TDSTK_UI;

public class Garage : MonoBehaviour{
    public static Action OnEnterGarge,OnExitGarage;
    public bool IsQuestChainEnd{get;private set;}
    
    private void Awake() {
        IsQuestChainEnd = false;
        
    }
    private void OnQuestChainEnd(){
        IsQuestChainEnd = true;
    }
    private void OnTriggerEnter(Collider other) {
        if(!other.TryGetComponent(out UnitPlayer player)) return;

        if(other.TryGetComponent(out Inventory inventory)) inventory.ClearInventory();
        
        if(!UIWeaponAbilityTab.IsOn()) UIWeaponAbilityTab.TurnTabOn();
        
        OnEnterGarge?.Invoke();
    }  
    
    private void OnTriggerExit(Collider other) {
        OnExitGarage?.Invoke();
    }
    
}
