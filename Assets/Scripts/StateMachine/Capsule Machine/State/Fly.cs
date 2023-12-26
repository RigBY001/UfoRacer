using HFSM;

namespace CapsuleState{
public class Fly : State{
    private Capsule _capsule;
    public Fly(Capsule capsule){
        _capsule = capsule;
    }
    
}
}
