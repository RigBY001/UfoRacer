using System;
using HFSM;

namespace GarageState{
    public class Opening : State{
    private Garage _garage;
    private Timer _timer;
    public Action OnOpened;

    public Opening(Garage garage){
        _garage = garage;
        _timer = new(2f);
        _timer.timeOut.AddListener(()=>OnOpened?.Invoke());
    }
    protected override void OnEnter(){
        _garage.ProgressBar.visible = true;
        _garage.ProgressBar.SetValue(0);
        _timer.Start();
    }
    protected override void OnUpdate(){
        if(_garage.Player == null) return;
        _garage.ProgressBar.SetValue(_timer.Progress());
    }
    protected override void OnExit(){
        _timer.Stop();
        _garage.ProgressBar.visible = false;
    }  
}
}