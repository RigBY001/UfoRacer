using HFSM;

namespace ZoneState{
public class Empty : State{
    private MiningZone _miningZone;

    public Empty(MiningZone miningZone){
        _miningZone = miningZone;
    }
    protected override void OnEnter(){
        _miningZone.Auger.Disable();
        _miningZone.ZoneOfOre.SetActive(true);
    }
}
}