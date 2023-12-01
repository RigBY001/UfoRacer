using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using TMPro;

public class QuestController : MonoBehaviour{
    [SerializeField] private List<QuestsChain>  _questsChains;
    [SerializeField]private TextMeshProUGUI _questMonitor;

    private List<Transform> _points;
    private LinkedList<QuestsZone> _questsZonesInGame;
    private Dictionary<QuestsZone,Quest> _questInfo;
    private QuestsChain _currentQuestChain;
    public bool IsQuestStart=>_currentQuestChain != null;
    private LinkedListNode<QuestsZone> _currentQuestZone;
    private const string _lastQuestGoal = "Back to base";  
    public Action OnQuestStart,OnQuestEnd;
    public static Action OnQuestChainEnd;

    private void Awake() {
        _points = new ();
        _questsZonesInGame = new();
        foreach(Transform child in transform) _points.Add(child);
        StartQuest();
        Garage.OnEnterGarge+= StartQuest;
    }
    private void OnDestroy() {
        Garage.OnEnterGarge-=StartQuest;
    }
    public void StartQuest(){
        if(IsQuestStart || _points.Count == 0 || _questsChains.Count == 0) return;

        _currentQuestChain = _questsChains[UnityEngine.Random.Range(0,_questsChains.Count)];
        if(_currentQuestChain == null) return;
        StartCoroutine(InitializeQuest()); 
    }
    private void StartQuestHandler(){

    }
    private void EndQuestHandler(){
        TurnOffQuest();
        TurnOnNextQuest();
    }
    private void TurnOnQuest(){
        _currentQuestZone.Value.gameObject.SetActive(true);
        SetQuestText();
        _currentQuestZone.Value.OnStartQuest+=StartQuestHandler;
        _currentQuestZone.Value.OnEndQuest+= EndQuestHandler;
    }
    private void TurnOffQuest(){
        _currentQuestZone.Value.OnStartQuest-=StartQuestHandler;
        _currentQuestZone.Value.OnEndQuest-= EndQuestHandler;
    }
    private void CancelQuestChain(){
        _questMonitor.text = _lastQuestGoal;
        OnQuestChainEnd?.Invoke();
        _currentQuestZone = null;
        _currentQuestChain = null;
    }
    private void TurnOnNextQuest(){
        _currentQuestZone = _currentQuestZone.Next;
        if(_currentQuestZone == null) {
            CancelQuestChain();
            return;
        }
        TurnOnQuest();
    }
    private IEnumerator InitializeQuest(){
        yield return StartCoroutine(QuestZoneDestroy());
        yield return StartCoroutine(QuestZoneCreator());
        _currentQuestZone = _questsZonesInGame.First;
        TurnOnQuest();
    }
    private IEnumerator QuestZoneDestroy(){
        if(_questsZonesInGame.Count >0) foreach(QuestsZone initZone in _questsZonesInGame) Destroy(initZone.gameObject);
        yield return null;
    }
    private IEnumerator QuestZoneCreator(){
        List<Quest> quests = new (_currentQuestChain.Quests); List<Transform> tempPoints = new(_points); List<QuestsZone> questsZones = new(); 
        Transform tempPoint; QuestsZone questsZone; _questInfo = new();

        foreach(Quest quest in quests){
            if(quest.QuestsZone == null) continue; if(tempPoints.Count == 0) continue;

            tempPoint = tempPoints[UnityEngine.Random.Range(0,tempPoints.Count)];
            questsZone = Instantiate(quest.QuestsZone);questsZone.transform.position = tempPoint.position;
            tempPoints.Remove(tempPoint);
            questsZones.Add(questsZone);
            _questInfo.Add(questsZone,quest);
            yield return null;
        }

        _questsZonesInGame  =  new(questsZones);
        foreach(QuestsZone zone in questsZones) zone.gameObject.SetActive(false);
    } 

    private void SetQuestText(){ 
        if(_questMonitor == null) return;
        _questMonitor.text = _questInfo[_currentQuestZone.Value].Text;
    }  
}
