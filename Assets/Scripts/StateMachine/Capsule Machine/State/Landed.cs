using System;
using HFSM;
using UnityEngine;

namespace CapsuleState{
public class Landed : State{
    private Capsule _capsule;
    
    public Landed(Capsule capsule){
        _capsule = capsule;
    }
    protected override void OnEnter(){
        Debug.Log("Lended On enter");
    }
   
}
}
