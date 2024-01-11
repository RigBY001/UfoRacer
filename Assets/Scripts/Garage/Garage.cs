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
    [SerializeField] private StorageMonitor _oreMonitor;
    public static Action OnEnterGarge,OnExitGarage;
    public IntStorage OreStorage{get;private set;}
    public IntStorage GoldStorage{get;private set;}
    public bool IsQuestChainEnd{get;private set;}
    
    public static Garage Instance;

    private GarageStateMachine _stateMachine;
    public GameObject Player{get;private set;}

    private void Awake() {
        IsQuestChainEnd = false;
        OreStorage = new(int.MaxValue);
        OreStorage.Value = 200;
        GoldStorage = new(int.MaxValue);
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
        playerEnter.AddTransition(closed,()=>Player == null);
        
        opening.OnOpened += opening.AddEventTransition(playerEnter);
        _oreMonitor.Init(OreStorage);
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
    public void PrepareSellOre(){
        PlayersManager.Instance.SetPlayer2();
        PlayersManager.Instance.CurrentPlayer.Inventroy.AddOre(UnloadOre());
    }
    public int UnloadOre(){
        int unloadOre = OreStorage.Value;
        OreStorage.Value = 0;
        return unloadOre;
    }
   
}
