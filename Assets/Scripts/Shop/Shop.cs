using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDSTK;

public class Shop : MonoBehaviour{
    [SerializeField] private StorageMonitor _goldMonitor;
    private List<string> _boughtAbility;
    private List<string> _boughtWeapon;
    public Storage<int> Gold {get;private set;}
    public static Shop Instance;
    
    private void Awake() {
        Instance = this;
        _boughtAbility = new();
        _boughtWeapon = new();
        Gold = new();
    }
    private void Start() {
        _boughtAbility.Add(AbilityManager.GetSelectedAbility().name);
        UnitPlayer player=GameControl.GetPlayer();
        _boughtWeapon.Add(player.weaponList[player.weaponID].weaponName);
        _goldMonitor.Init(Gold);
    }
    public void AddGold(int gold){
        Gold.Value += gold;
    }
    public List<string> BoughtAbility()=>new(_boughtAbility);
    public List<string> BoughtWeapon()=>new(_boughtWeapon);

    public bool HasBoughtAbility(string name)=>_boughtAbility.Contains(name);
    public bool HasBoughtWeapon(string name)=>_boughtWeapon.Contains(name);
    public bool CanBuy(int cost)=>cost<=Gold.Value;
    public bool BuyAbility(int cost,string name){
        if(cost>Gold.Value) return false;
        Gold.Value-=cost;
        _boughtAbility.Add(name);
        return true;
    }
    public bool BuyWeapon(int cost,string name){
        if(cost>Gold.Value) return false;
        Gold.Value-=cost;
        _boughtWeapon.Add(name);
        return true;
    }
}
