using System;
using HFSM;

namespace AugerState{
public class ExtractOre : State{
    private Auger _auger;
    private Inventory _playerInventory;
    private Timer _timer;
    public Action OnDoneUnloading;
    
    public ExtractOre(Auger auger){
        _auger = auger;
        _timer = new (0.1f);
    }
   
    protected override void OnEnter(){
        _playerInventory = _auger.Player.GetComponent<Inventory>();
        _timer.Start();
    }
    protected override void OnUpdate(){
        if(_auger.Player == null || _playerInventory == null || _timer.IsRunning()) return;
        _playerInventory.AddOre(_auger.UnloadingOre());
        if(_auger.IsEmpty) OnDoneUnloading.Invoke();
        _timer.Start();
    }
     protected override void OnExit(){
        _timer.Stop();
    }
}
}
