using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CapsuleState;
using System;

public class Capsule : MonoBehaviour{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Scaner _playerScaner;
    [SerializeField] private GameObject _model;
    [SerializeField] private Shop _shop;
    public Shop Shop {get { return _shop;}}

    private CapsuleStateMachine _stateMachine;
    public CapsuleStateMachine StateMachine{get{ return _stateMachine;}}
    
    public Action OnLanding;
    private Transform _transform;
    public bool IsActive{get;private set;}
    public GameObject Player{get;private set;}

    private void Awake() {
        Fly fly = new(this);
        Landing landing = new(this);
        Landed landed = new(this);
        ExchangeOre exchangeOre = new(this);

        _stateMachine = new (landing,landed,exchangeOre,fly);
        landing.OnLanded += landing.AddEventTransition(landed);
        landed.AddTransition(exchangeOre,()=>Player != null);
        exchangeOre.AddTransition(landed,()=>Player == null);
        _transform = transform;
    }
    private void Start() {
        _playerScaner.OnEnter += OnPlayerEnter;
        _playerScaner.OnExit += OnPlayerExit;
    }
    public void Enable(){
        _model.SetActive(true);
        IsActive = true;
    }
    public void Disable(){
        _model.SetActive(false);
        IsActive = false;
    }
     private void OnPlayerEnter(){
        Player = _playerScaner.ScanedObject;
    }
    private void OnPlayerExit(){
        Player = null;
    }
    
    public void Landing(float progress){
        _model.transform.position = Vector3.Lerp(_startPoint.position,_endPoint.position,progress);
    }
}
