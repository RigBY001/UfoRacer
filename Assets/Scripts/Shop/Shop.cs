using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDSTK;

public class Shop : MonoBehaviour{
    private List<string> _boughtAbility;
    private List<string> _boughtWeapon;
    
    private int _bill;

    private void Awake() {
        _boughtAbility = new();
        _boughtWeapon = new();
    }
    private void Start() {
        _boughtAbility.Add(AbilityManager.GetSelectedAbility().name);
        UnitPlayer player=GameControl.GetPlayer();
        _boughtWeapon.Add(player.weaponList[player.weaponID].weaponName);
    }
    public List<string> BoughtAbility()=>new(_boughtAbility);
    public List<string> BoughtWeapon()=>new(_boughtWeapon);

    public bool HasBoughtAbility(string name)=>_boughtAbility.Contains(name);
    public bool HasBoughtWeapon(string name)=>_boughtWeapon.Contains(name);
    public bool CanBuy(int cost)=>cost<=_bill;
    public bool BuyAbility(int cost,string name){
        if(cost>_bill) return false;
        _bill-=cost;
        _boughtAbility.Add(name);
        return true;
    }
    public bool BuyWeapon(int cost,string name){
        if(cost>_bill) return false;
        _bill-=cost;
        _boughtWeapon.Add(name);
        return true;
    }
}
