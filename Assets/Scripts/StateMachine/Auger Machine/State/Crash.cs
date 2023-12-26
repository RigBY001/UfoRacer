using HFSM;
namespace AugerState{
public class Crash : State{
    private Auger _auger;
    public bool IsDone{get;private set;}

    public Crash(Auger auger){
        _auger = auger;
        IsDone = false;
    }
    protected override void OnEnter(){
        _auger.BoxOfOre.SetActive(false);
        IsDone = true;
        _auger.OnCrash?.Invoke();
    }  
    protected override void OnExit(){
        IsDone = false;
        _auger.Disable();
    }    
}
}