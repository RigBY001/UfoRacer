using System.Collections;
using System.Collections.Generic;
using MoreMountains.HighroadEngine;
using TDSTK;
using UnityEngine;

public class PlayerComponent {
    private GameObject _gameObject;
    public GameObject GameObject=>_gameObject;
    private UnitPlayer _unitPlayer;
    public UnitPlayer UnitPlayer => _unitPlayer;
    private Inventory _inventroy;
    public Inventory Inventroy=>_inventroy;
    private SolidController _solidController;
    public SolidController SolidController=>_solidController;

    public PlayerComponent (GameObject player){
        _gameObject = player;
        _unitPlayer =  player.GetComponent<UnitPlayer>();
        _inventroy = player.GetComponent<Inventory>();
        _solidController = player.GetComponent<SolidController>();
    }
}
