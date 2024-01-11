using System;
using UnityEngine;

public class DayCounter : MonoBehaviour{
    [SerializeField] private float _dayTimeSec;
    private Timer _timer;
    private int _day;
    public Action OnStartDay,OnEndDay;

    private void Awake() {
        _timer = new(_dayTimeSec);
    }
    private void Start() {  
        _timer.Loop = true;
        _timer.timeOut.AddListener(EndDay);
        _timer.Start();
        StartDay();
    }
    private void StartDay(){
        OnStartDay?.Invoke();
    }
    private void EndDay(){
        OnEndDay?.Invoke();
        _day++;
        StartDay();
    }
    private void OnDestroy() {
        _timer.Stop();
    }
}
