using TDSTK;

public class BreakeItem : QuestConnector{
   private void Start() {
     if(TryGetComponent(out UnitAI unit)) unit.SetDestroyCallback(DestroyCallback);
   }
   private void DestroyCallback(){
     _indicator.SetActive(false);
     Zone.EndQuest();
   }
}
