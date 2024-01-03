using UnityEngine;

public class ExitZone : MonoBehaviour{
     [SerializeField] private Scaner _playerScaner;
     [SerializeField] private SellArea _sellArea;
     
     private void Start() {
          _playerScaner.OnEnter+=OnPlayerEnter;
          _playerScaner.OnExit+=OnPlayerExit;
     }

     private void OnPlayerEnter(){
          _sellArea.EndArea();
     }
     private void OnPlayerExit(){
     }
}
