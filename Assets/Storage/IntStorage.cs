using System;

public class IntStorage: Storage<int> {
    private int _maxValue;
    public bool IsMax()=> Value == _maxValue;
    public new Action<IntStorage> OnChangeValue;
    public IntStorage(int maxValue){
        _maxValue = maxValue;
    }
    protected override void SetValue(int value){
        base.SetValue(UnityEngine.Mathf.Clamp(value,0,_maxValue));
        OnChangeValue?.Invoke(this);
    }
}
