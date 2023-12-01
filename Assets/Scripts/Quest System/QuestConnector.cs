using UnityEngine;

public class QuestConnector :MonoBehaviour{
   [HideInInspector]
   public QuestsZone Zone;
   [SerializeField] protected GameObject _indicator;
   public virtual void Awake() {
      Zone= GetComponentInParent<QuestsZone>();
   }
}
