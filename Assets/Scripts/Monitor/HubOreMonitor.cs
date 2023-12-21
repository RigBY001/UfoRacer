using TMPro;
using UnityEngine;

public class HubOreMonitor : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI _monitor;

    public void Init<T>(Storage<T> storage){
        storage.OnChangeValue += ()=> _monitor.text = storage.Value.ToString();
    }
}
