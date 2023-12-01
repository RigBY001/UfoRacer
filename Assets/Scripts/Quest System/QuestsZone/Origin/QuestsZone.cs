using System;
using UnityEngine;

public class QuestsZone:MonoBehaviour {
     public Action OnStartQuest,OnEndQuest;
     
     public virtual void StartQuest(){
          OnStartQuest?.Invoke();
     }
     public virtual void EndQuest(){
          OnEndQuest?.Invoke();
     }    
}
