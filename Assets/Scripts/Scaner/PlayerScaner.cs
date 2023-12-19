using System;
using UnityEngine;

public class PlayerScaner : MonoBehaviour{
    public Action OnPlayerEnter,OnPlayerExit;
    public GameObject Player {get;private set;}
    
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")) return;
        Player = other.gameObject;
        OnPlayerEnter?.Invoke();
    }
    private void OnTriggerExit(Collider other) {
        if(!other.CompareTag("Player")) return;
        Player = null;
        OnPlayerExit?.Invoke();
    }
    
}
