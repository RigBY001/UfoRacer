using System;
using UnityEngine;

public class Scaner : MonoBehaviour{
    public Action OnEnter,OnExit;
    public GameObject ScanedObject {get;private set;}
    [SerializeField] private string _tag;

    public void SetScanerTag(string tag){
        _tag = tag;
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag(_tag)) return;
        ScanedObject = other.gameObject;
        OnEnter?.Invoke();
    }
    private void OnTriggerExit(Collider other) {
        if(!other.CompareTag(_tag)) return;
        ScanedObject = null;
        OnExit?.Invoke();
    }
    
}
