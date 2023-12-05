using HFSM;

public class Initial : State{
    private MiningZone _miningZone;

    public Initial(MiningZone miningZone){
        _miningZone = miningZone;
    }

    protected override void OnEnter(){
        _miningZone.ZoneOfOre.SetActive(false);
        _miningZone.Auger.SetActive(true);
    }
}
