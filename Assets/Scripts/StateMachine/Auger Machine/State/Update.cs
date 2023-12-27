using System;
using HFSM;
using UnityEngine;

public class Update : State{
    private Auger _auger;
    private Timer _timer;
    public Action OnUpdated;
    public Update(Auger auger){
        _auger = auger;
        _timer = new(3f);
        _timer.timeOut.AddListener(()=>OnUpdated?.Invoke());
    }
    protected override void OnEnter(){
        _auger.ProgressBar.visible = true;
        _auger.ProgressBar.SetValue(0);
        _timer.Start();
    }
    protected override void OnUpdate(){
        if(_auger.Player == null) return;
        _auger.ProgressBar.SetValue(_timer.Progress());
    }
    protected override void OnExit(){
        _timer.Stop();
        _auger.ProgressBar.visible = false;
    }  
}
