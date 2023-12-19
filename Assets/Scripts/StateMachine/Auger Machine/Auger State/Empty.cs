using HFSM;

namespace AugerState{
public class Empty : State{
    private Auger _auger;
    
    public Empty(Auger auger){
        _auger = auger;
    }
    protected override void OnEnter(){
        _auger.BoxOfOre.SetActive(false);
    }
}
}