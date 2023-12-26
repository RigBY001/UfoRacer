using System;
using HFSM;

namespace SellZoneState{
public class WaitingCapsule : State{
    private SellZone _sellZone;
    private Timer _timer;
    public Action OnTimeOut;
    
    public WaitingCapsule(SellZone sellZone){
        _sellZone  = sellZone;
        _timer = new Timer(3f);
        _timer.timeOut.AddListener(()=>OnTimeOut?.Invoke());
    }
    protected override void OnEnter(){
        _sellZone.ProgressBar.visible = true;
        _sellZone.ProgressBar.SetValue(0);
        _timer.Start();
    }
    protected override void OnUpdate(){
        _sellZone.ProgressBar.SetValue(_timer.Progress());
    }
    protected override void OnExit(){
        _timer.Stop();
        _sellZone.ProgressBar.visible = false;
    } 
}
}