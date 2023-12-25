using HFSM;

namespace GarageState{
public class Closed : State{
    private Garage _garage;

    public Closed(Garage garage){
        _garage = garage;
        
    }
    
}
}
