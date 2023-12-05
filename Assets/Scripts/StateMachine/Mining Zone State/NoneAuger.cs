using HFSM;

public class NoneAuger : State{
    private MiningZone _miningZone;

    public NoneAuger(MiningZone miningZone){
        _miningZone = miningZone;
    }
    protected override void OnEnter(){
        
        _miningZone.ZoneOfOre.SetActive(true);
    }
}
