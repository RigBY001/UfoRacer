using UnityEngine;

public class SellArea : MonoBehaviour{
    [SerializeField] private QuestController _questController;
    [SerializeField] private ExitZone _exitZone;

    public void StartArea(){
        _questController.StartQuest();
    }
    public void EndArea(){
        _questController.DisableQuestController();
    }
}
