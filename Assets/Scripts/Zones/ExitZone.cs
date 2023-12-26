using System;
using UnityEngine;
using UnityEngine.Events;

public class ExitZone : MonoBehaviour{
   public Action<Collider> OnEnter,OnExit;
   private void OnTriggerEnter(Collider other) {
        OnEnter?.Invoke(other);
   }
   private void OnTriggerExit(Collider other) {
        OnExit?.Invoke(other);
   }
}
