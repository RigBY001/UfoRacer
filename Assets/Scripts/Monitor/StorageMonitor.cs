using TMPro;
using UnityEngine;

public class StorageMonitor : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI _monitor;

    public void Init<T>(Storage<T> storage){
        _monitor.text = storage.Value.ToString();
        storage.OnChangeValue += ()=> _monitor.text = storage.Value.ToString();
    }
}
