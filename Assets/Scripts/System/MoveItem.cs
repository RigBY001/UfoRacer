using UnityEngine;

public class MoveItem : MonoBehaviour{
    public bool IsEnd{get;private set;}

    [SerializeField] private float _speed;

    private Transform _parent,_transform;
    private Vector3 _endPoint;
    private void Awake() {
        _transform = transform;
    }
    public void GoUpFromParent(Transform parent, float distance){
        _endPoint = parent.position;
        _endPoint.y += distance;
        _parent = parent;    
        IsEnd = false;
    }
    public void GoDownToParent(Transform parent, float distance){
        Vector3 position = parent.position;
        _endPoint = position;
        position.y += distance;
        _transform.position = position;
        _parent = parent;
        IsEnd = false;
        transform.parent = parent;
    }
    private void Update() {
        if(_parent == null || IsEnd) return;
        _transform.position = Vector3.MoveTowards(_transform.position,_endPoint,_speed*Time.deltaTime);
        if(_transform.position == _endPoint){
            IsEnd = true;
            _parent = null;
        }
    }
}
