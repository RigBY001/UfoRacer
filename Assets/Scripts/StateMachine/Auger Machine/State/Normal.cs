using System;
using HFSM;

namespace AugerState{
public class Normal:State{
    private Auger _auger;
    private Timer _timer;
    public Action OnFulled;
    public Normal(Auger miningZone){
        _auger = miningZone;
        _timer = new Timer(5f);
        _timer.timeOut.AddListener(()=>OnFulled?.Invoke());
    }
     protected override void OnEnter(){
        _auger.ProgressBar.visible = true;
        _auger.ProgressBar.SetValue(0);
        _timer.Start();
    }
    protected override void OnUpdate(){
        _auger.ProgressBar.SetValue(_timer.Progress());
    }
    protected override void OnExit(){
        _timer.Stop();
        _auger.ProgressBar.visible = false;
    } 
}
}
