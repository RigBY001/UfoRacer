using System;
using HFSM;

public class Installed : State{
    private MiningZone _miningZone;
    public Action OnInstaled;

    public Installed(MiningZone miningZone){
        _miningZone = miningZone;
    }
    protected override void OnEnter(){
        _miningZone.Auger.Eneble();
        _miningZone.ZoneOfOre.SetActive(false);
        OnInstaled?.Invoke();
    }
}
