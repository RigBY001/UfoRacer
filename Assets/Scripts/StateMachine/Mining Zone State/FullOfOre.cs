using HFSM;

public class FullOfOre : State{
    private MiningZone _miningZone;

    public FullOfOre(MiningZone miningZone){
        _miningZone = miningZone;
    }   

    protected override void OnEnter(){
        _miningZone.BoxOfOre.SetActive(true);
    }
}
