using System;
using HFSM;
using UnityEngine;

namespace CapsuleState{
public class Landing : State{
    private Capsule _capsule;
    private Timer _timer;
    public Action OnLanded;
    
    public Landing(Capsule capsule){
        _capsule = capsule;
        _timer  = new(3f);
        _timer.timeOut.AddListener(()=>OnLanded?.Invoke());
    }
    protected override void OnEnter(){
        _timer.Start();
    }
    protected override void OnUpdate(){
        _capsule.Landing(_timer.Progress());
    }
    protected override void OnExit(){
        _timer.Stop();
    } 
}
}
