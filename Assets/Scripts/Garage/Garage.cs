using System;
using UnityEngine;
using TDSTK;
using MoreMountains.HighroadEngine;
using GarageState;
using HUDIndicator;

public class Garage : MonoBehaviour{
    [SerializeField] IndicatorBarOnScreen _progerssBar;
    public IndicatorBarOnScreen ProgressBar=>_progerssBar;
    [SerializeField] private Scaner _playerScaner;
    [SerializeField] private UnitPlayer _unitPlayer1;
    [SerializeField] private UnitPlayer _unitPlayer2;
    [SerializeField] private Monitor _oreMonitor;
    public static Action OnEnterGarge,OnExitGarage;
    public Storage<int> OreStorege{get;private set;}
    public Storage<int> GoldStorege{get;private set;}
    public bool IsQuestChainEnd{get;private set;}
    
    public static Garage Instance;

    private GarageStateMachine _stateMachine;
    public GameObject Player{get;private set;}

    private void Awake() {
        IsQuestChainEnd = false;
        OreStorege = new();
        GoldStorege = new();
        Instance = this;
    }
    private void Start() {
        Closed closed = new(this);
        Opening opening = new(this);
        PlayerEnter playerEnter = new(this);

        _stateMachine = new(closed,opening,playerEnter);
        _stateMachine.Init();

        closed.AddTransition(opening,()=>Player != null);
        opening.AddTransition(closed,()=>Player == null);

        opening.OnOpened += opening.AddEventTransition(playerEnter);
        
        _unitPlayer1.gameObject.GetComponent<SolidController>().EnableControls(1);
        GameControl.SetPlayer(_unitPlayer1);
        _oreMonitor.Init(OreStorege);

        _playerScaner.OnEnter += OnPlayerEnter;
        _playerScaner.OnExit  += OnPlayerExit;
    }
    private void OnPlayerEnter(){
        Player = _playerScaner.ScanedObject;
    }
    private void OnPlayerExit(){
        Player = null;
    }
    private void OnQuestChainEnd(){
        IsQuestChainEnd = true;
    }
    private void Update(){
        _stateMachine.Update();
    }
    public void ChangePlayer(){
        UnitPlayer currentPlayer = GameControl.GetPlayer();
        if(currentPlayer != _unitPlayer1) {
            GameControl.SetPlayer(_unitPlayer1);
            var controller =  _unitPlayer2.gameObject.GetComponent<SolidController>();
            controller.DisableControls();
            controller =  _unitPlayer1.gameObject.GetComponent<SolidController>();
            controller.EnableControls(1);
            Debug.Log(GameControl.GetPlayer().name);
            return;
        }
        if(currentPlayer != _unitPlayer2) {
            GameControl.SetPlayer(_unitPlayer2);
            var controller =  _unitPlayer1.gameObject.GetComponent<SolidController>();
            controller.DisableControls();
            controller =  _unitPlayer2.gameObject.GetComponent<SolidController>();
            controller.EnableControls(2);
            Debug.Log(GameControl.GetPlayer().name);
            return;
        }
    }
}
