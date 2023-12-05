using HFSM;

public class AugerCrash : State{
    private MiningZone _miningZone;

    public AugerCrash(MiningZone miningZone){
        _miningZone = miningZone;
    }
    protected override void OnEnter(){
        _miningZone.ZoneOfOre.SetActive(true);
        _miningZone.Auger.SetActive(false);
        _miningZone.BoxOfOre.SetActive(false);
    }   
}
