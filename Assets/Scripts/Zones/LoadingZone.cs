using System.Collections;
using System.Collections.Generic;
using TDSTK_UI;
using UnityEngine;

public class LoadingZone : MonoBehaviour{
    [SerializeField] private Garage _garage;
    [SerializeField] private Scaner _playerScaner;
    [SerializeField] private SellArea _sellArea;

    private void Start() {
        _playerScaner.OnEnter+=OnPlayerEnter;
        _playerScaner.OnExit+=OnPlayerExit;
    }
    private void OnPlayerEnter(){
            if(_garage.OreStorage.Value >0) UISellOre.Show();
    }
    private void OnPlayerExit(){
            if(UISellOre.IsActive()) UISellOre.Hide();
    }
    public void LoadingOre(){
            _garage.PrepareSellOre();
            _sellArea.StartArea();
            if(UISellOre.IsActive()) UISellOre.Hide();
    }
}
