using HFSM;

namespace AugerState{
public class Broken : State{
    private Auger _auger;

    public Broken(Auger auger){
        _auger = auger;
    }  

    protected override void OnEnter(){
        _auger.BoxOfOre.SetActive(false);
        _auger.AugerBrokenIndicator.SetActive(true);
    }
    protected override void OnUpdate(){
        
    }
    protected override void OnExit(){
        _auger.AugerBrokenIndicator.SetActive(false);
    }
}
}