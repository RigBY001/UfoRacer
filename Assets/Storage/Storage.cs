using System;

public class Storage<T>{
    private T _value;
    public T Value {get{return _value;} set{_value = value; OnChangeValue?.Invoke(); }}
    public Action OnChangeValue;
}

