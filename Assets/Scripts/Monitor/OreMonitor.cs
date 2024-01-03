using System;
using TMPro;
using UnityEngine;

public class OreMonitor : MonoBehaviour{
    private TextMeshProUGUI _oreMonitor;
    private IntStorage _currentStorage;
    public static OreMonitor Instance;
    
    private void OnValidate() {
        _oreMonitor = GetComponent<TextMeshProUGUI>();
    }
    private void Awake() {
        _oreMonitor.text = "";
        Instance = this;
    }
    public void SetMonitor(IntStorage storage){
        if(_currentStorage != null)  storage.OnChangeValue -= OnCahngeOre;
        storage.OnChangeValue += OnCahngeOre;
        _oreMonitor.text = storage.Value.ToString();
        _currentStorage = storage;
    }
    private void OnCahngeOre(IntStorage storage){
        _oreMonitor.text = storage.Value.ToString();
    }
}
