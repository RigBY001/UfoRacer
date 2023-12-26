using System;
using HFSM;
using UnityEngine;

namespace CapsuleState{
public class ExchangeOre : State{
    private Capsule _capsule;

    
    public ExchangeOre(Capsule capsule){
        _capsule = capsule;
    }
    protected override void OnEnter(){
         Inventory inventory = _capsule.Player.GetComponent<Inventory>();
        _capsule.Shop.AddGold(inventory.UnloadOre());
    }
 
}
}
