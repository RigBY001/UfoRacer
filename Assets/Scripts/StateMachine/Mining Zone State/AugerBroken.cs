using HFSM;

public class AugerBroken : State{
    private MiningZone _miningZone;

    public AugerBroken(MiningZone miningZone){
        _miningZone = miningZone;
    }  

    protected override void OnEnter(){
        _miningZone.BoxOfOre.SetActive(false);
        _miningZone.AugerBrokenIndicator.SetActive(true);
    }
    protected override void OnExit(){
        _miningZone.AugerBrokenIndicator.SetActive(false);
    }
}
