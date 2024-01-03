using System;

public class Storage<T>{
    protected T _value;
    public T Value {get{return _value;} set{SetValue(value);}}
    public Action OnChangeValue;

    protected virtual void SetValue(T value){
        _value = value;
        OnChangeValue?.Invoke();
    }
}

