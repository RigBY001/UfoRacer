using HUDIndicator;
using SellZoneState;
using UnityEngine;

public class SellZone : QuestConnector{
    [SerializeField] private Scaner _playerScaner;
    [SerializeField] IndicatorBarOnScreen _progerssBar;
    public IndicatorBarOnScreen ProgressBar=>_progerssBar;
    [SerializeField] private Capsule _capsule;
    public Capsule Capsule=>_capsule;

    public GameObject Player {get;private set;}
    private SellZoneStateMachine _stateMachine;

    private void Start() {
        Normal normal = new (this);
        WaitingCapsule waitingCapsule = new(this);
        PostSignalCapsule postSignalCapsule = new(this);

        _stateMachine = new(normal,waitingCapsule,postSignalCapsule,_capsule.StateMachine);
        _stateMachine.Init();

        normal.AddTransition(waitingCapsule,()=> Player != null);
        waitingCapsule.OnTimeOut += waitingCapsule.AddEventTransition(postSignalCapsule);
        postSignalCapsule.AddTransition(_capsule.StateMachine,()=>_capsule.IsActive);

        _playerScaner.OnEnter += OnPlayerEnter;
        _playerScaner.OnExit += OnPlayerExit;
    }
    private void OnPlayerEnter(){
        Player = _playerScaner.ScanedObject;
    }
    private void OnPlayerExit(){
        Player = null;
    }
    private void Update() {
        _stateMachine.Update();
    }
}
