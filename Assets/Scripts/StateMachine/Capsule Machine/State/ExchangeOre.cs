using HFSM;

namespace CapsuleState
{
    public class ExchangeOre : State{
    private Capsule _capsule;
    
    public ExchangeOre(Capsule capsule){
        _capsule = capsule;
    }
    protected override void OnEnter(){
        Inventory inventory = PlayersManager.Instance.CurrentPlayer.Inventroy;
        inventory.AddGold(inventory.UnloadOre());
    }
 
}
}
