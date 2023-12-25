using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageHub : MonoBehaviour{
    public void StartRade(){
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}
