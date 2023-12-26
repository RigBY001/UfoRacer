using HFSM;

namespace SellZoneState{
public class Normal : State{
    private SellZone _sellZone;
    
    public Normal(SellZone sellZone){
        _sellZone  = sellZone;
    }
    
 
}
}