using HFSM;

namespace SellZoneState{
public class PostSignalCapsule : State{
    private SellZone _sellZone;
    
    public PostSignalCapsule(SellZone sellZone){
        _sellZone  = sellZone;
    }

    protected override void OnEnter(){
        _sellZone.Capsule.Enable();
    }
    protected override void OnExit(){
    }
}
}