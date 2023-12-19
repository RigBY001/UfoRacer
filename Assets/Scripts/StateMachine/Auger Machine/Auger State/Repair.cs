using System;

using HFSM;

public class Repair : State{
    private Auger _auger;
    private Timer _timer;
    public Action OnRepair;
    
    public Repair(Auger auger){
        _auger = auger;
        _timer = new(1f);
        _timer.timeOut.AddListener(()=>OnRepair?.Invoke());
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
