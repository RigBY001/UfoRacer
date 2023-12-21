using System;
using UnityEngine;

public class Scaner : MonoBehaviour{
    public Action OnPlayerEnter,OnPlayerExit;
    public GameObject ScanedObject {get;private set;}
    [SerializeField] private string _tag;

    public void SetScanerTag(string tag){
        _tag = tag;
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag(_tag)) return;
        ScanedObject = other.gameObject;
        OnPlayerEnter?.Invoke();
    }
    private void OnTriggerExit(Collider other) {
        if(!other.CompareTag(_tag)) return;
        ScanedObject = null;
        OnPlayerExit?.Invoke();
    }
    
}
