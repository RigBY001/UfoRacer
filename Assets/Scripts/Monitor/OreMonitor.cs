using System;
using TMPro;
using UnityEngine;

public class OreMonitor : MonoBehaviour{
    private TextMeshProUGUI _oreMonitor;
    private Inventory _playerInventory;
    
    private void OnValidate() {
        _oreMonitor = GetComponent<TextMeshProUGUI>();
    }
    private void Start() {
        var player = TDSTK.GameControl.GetPlayer();
        _playerInventory  = player.gameObject.GetComponent<Inventory>();
        if(_playerInventory== null) return;
        _playerInventory.Ore.OnChangeValue += OnCahngeOre;
        _oreMonitor.text = _playerInventory.Ore.Value.ToString();
    }
    private void OnCahngeOre(){
        _oreMonitor.text = _playerInventory.Ore.Value.ToString();
    }
}
