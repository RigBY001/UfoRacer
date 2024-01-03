using UnityEngine;
using UnityEngine.UI;

public class SellOreObjective : MonoBehaviour{

    [SerializeField] private Button _button;
    private void Start() {
        Garage.Instance.OreStorage.OnChangeValue += OnOreChange;
        OnOreChange(Garage.Instance.OreStorage);
    }

    private void OnOreChange(IntStorage storage){
        if(storage.Value <= 0){
            _button.gameObject.SetActive(false);
            return;
        }
        if(storage.IsMax()){
            _button.gameObject.SetActive(true);
            return;
        }
    }
}
