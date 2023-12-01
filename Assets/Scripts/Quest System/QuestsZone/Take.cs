public class Take : QuestsZone{
    private QuestConnector questConnector;
    private void Start() {
        questConnector = GetComponent<QuestConnector>();
        if(questConnector == null) return;
        questConnector.Zone = this;
    }
    
}

