using System.Collections;
using System.Collections.Generic;
using TDSTK;
using UnityEngine;

public class PlayersManager : MonoBehaviour{
    [SerializeField] private GameObject _player1;
    private PlayerComponent _player1Components;
    [SerializeField] private GameObject _player2;
    private PlayerComponent _player2Components;
    public static PlayersManager Instance;
    public PlayerComponent CurrentPlayer;
    private void Awake() {
        Instance = this;
        _player1Components = new(_player1);
        _player2Components = new(_player2);
        CurrentPlayer = _player1Components;
    }
    private void Start() {
        SetPlayer1();
    }
    public void SetPlayer1(){
        GameControl.SetPlayer(_player1Components.UnitPlayer);
        _player2Components.SolidController.DisableControls();
        _player1Components.SolidController.EnableControls(1);
        OreMonitor.Instance.SetMonitor(_player1Components.Inventroy.Ore);
        CurrentPlayer = _player1Components;
    }
    public void SetPlayer2(){
        GameControl.SetPlayer(_player2Components.UnitPlayer);
        _player1Components.SolidController.DisableControls();
        _player2Components.SolidController.EnableControls(2);
        OreMonitor.Instance.SetMonitor(_player2Components.Inventroy.Ore);
        CurrentPlayer = _player2Components;
    }
}
