using HFSM;

namespace AugerState{
public class FullOfOre : State{
    private Auger _auger;

    public FullOfOre(Auger auger){
        _auger = auger;
    }   

    protected override void OnEnter(){
        _auger.BoxOfOre.SetActive(true);
        _auger.MiningOre();
    }
}
}