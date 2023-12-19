using System;
using HFSM;

namespace ZoneState{
public class Builiding : State{
    private MiningZone _miningZone;

    private Timer _timer;
    public Action OnBuilded;

    public Builiding(MiningZone miningZone){
        _miningZone = miningZone;
        _timer = new (1f);
        _timer.timeOut.AddListener( ()=>OnBuilded?.Invoke());
    }

    protected override void OnEnter(){
        _miningZone.ProgressBar.visible = true;
        _miningZone.ProgressBar.SetValue(0);
        _timer.Start();
    }
    protected override void OnUpdate(){
        if(_miningZone.Player == null) return;
        _miningZone.ProgressBar.SetValue(_timer.Progress());
    }
    protected override void OnExit(){
        _timer.Stop();
        _miningZone.ProgressBar.visible = false;
    }
}
}
