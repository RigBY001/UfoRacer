using HFSM;

namespace AugerState{
public class Normal:State{
    private Auger _auger;

    public Normal(Auger miningZone){
        _auger = miningZone;
    }
    protected override void OnEnter(){
      
    }
    
}
}
