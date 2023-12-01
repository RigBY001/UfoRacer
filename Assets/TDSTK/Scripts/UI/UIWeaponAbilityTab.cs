using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK_UI{

	public class UIWeaponAbilityTab : MonoBehaviour {
		
		[SerializeField] private Shop _shop;
		private GameObject thisObj;
		private CanvasGroup canvasGroup;
		private static UIWeaponAbilityTab instance;
		//public static UIWeaponAbilityTab GetInstance(){ return instance; } 
		
		public void Awake(){
			instance=this;
			thisObj=gameObject;
			canvasGroup=thisObj.GetComponent<CanvasGroup>();
			if(canvasGroup==null) canvasGroup=thisObj.AddComponent<CanvasGroup>();
			
			thisObj.GetComponent<RectTransform>().anchoredPosition=new Vector3(0, 0, 0);
		}
		
		
		
		public GameObject tabObject;
		
		public List<UISelectItem> abilityItemList=new List<UISelectItem>();
		public List<UISelectItem> weaponItemList=new List<UISelectItem>();
		
		IEnumerator Start(){
			yield return null;
			
			if(!UIMainControl.EnableItemSelectTab()) thisObj.SetActive(false);
			
			List<Ability> abilityList=AbilityManager.GetAbilityList();
			
			for(int i=0; i<abilityList.Count; i++){
				if(i==0) abilityItemList[i].Init();
				else abilityItemList.Add(UISelectItem.Clone(abilityItemList[0].rootObj, "Item"+(i+1)));
				
				abilityItemList[i].imgIcon.sprite=abilityList[i].icon;
				abilityItemList[i].label.text=abilityList[i].name;
				
		
				abilityItemList[i].selectHighlight.SetActive(false);
				//abilityItemList[i].buttonObj.SetActive(false);
			}
			if(abilityList.Count==0) abilityItemList[0].rootObj.SetActive(false); 
			
			
			UnitPlayer player=GameControl.GetPlayer();
			
			for(int i=0; i<player.weaponList.Count; i++){
				if(i==0) weaponItemList[i].Init();
				else weaponItemList.Add(UISelectItem.Clone(weaponItemList[0].rootObj, "Item"+(i+1)));
				
				weaponItemList[i].imgIcon.sprite=player.weaponList[i].icon;
				weaponItemList[i].label.text=player.weaponList[i].weaponName;
				
				weaponItemList[i].selectHighlight.SetActive(false);
				//weaponItemList[i].buttonObj.SetActive(false);
			}
			
			tabObject.SetActive(false);
		}
		
		
		
		void OnEnable(){
			TDS.onNewWeaponE += OnNewWeapon;
			TDS.onNewAbilityE += OnNewAbility;
		}
		void OnDisable(){
			TDS.onNewWeaponE -= OnNewWeapon;
			TDS.onNewAbilityE -= OnNewAbility;
		}
		
		
		void OnNewWeapon(Weapon weapon, int replaceIndex=-1){
			if(replaceIndex>=0){
				weaponItemList[replaceIndex].imgIcon.sprite=weapon.icon;
				weaponItemList[replaceIndex].label.text=weapon.weaponName;
			}
			else{
				int index=weaponItemList.Count;
				weaponItemList.Add(UISelectItem.Clone(weaponItemList[0].rootObj, "Item"+(index)));
				
				weaponItemList[index].imgIcon.sprite=weapon.icon;
				weaponItemList[index].label.text=weapon.weaponName;
				
				weaponItemList[index].rootObj.SetActive(true); 
				weaponItemList[index].selectHighlight.SetActive(false);
			}
		}
		void OnNewAbility(Ability ability, int replaceIndex=-1){
			if(replaceIndex>=0){
				abilityItemList[replaceIndex].imgIcon.sprite=ability.icon;
				abilityItemList[replaceIndex].label.text=ability.name;
			}
			else{
				int index=abilityItemList.Count;
				abilityItemList.Add(UISelectItem.Clone(abilityItemList[0].rootObj, "Item"+(index)));
				
				abilityItemList[index].imgIcon.sprite=ability.icon;
				abilityItemList[index].label.text=ability.name;
				
				abilityItemList[index].rootObj.SetActive(true); 
				abilityItemList[index].selectHighlight.SetActive(false);
			}
		}
		
		
		
		void UpdateTab(){
			
			// for(int i=0; i<AbilityManager.GetAbilityCount(); i++){
			// 	if(i==AbilityManager.GetSelectID()){
			// 		abilityItemList[i].selectHighlight.SetActive(true);
			// 		abilityItemList[i].button.interactable=false;
			// 	}
			// 	else{
			// 		abilityItemList[i].selectHighlight.SetActive(false);
			// 		abilityItemList[i].button.interactable=true;
			// 	}
			// }

			List<string> boughtAbility = _shop.BoughtAbility();
			UIItemSelectFilter(ref abilityItemList,ref boughtAbility);

			List<Ability> abilities = Ability_DB.LoadClone();
			bool canBuy ;
			foreach(UISelectItem item in abilityItemList){
				foreach(Ability ability in abilities){
						if(ability.name == item.label.text & !boughtAbility.Contains(ability.name)){
							item.buttonText.text = ability.ShopCost.ToString();
							canBuy = _shop.CanBuy(ability.ShopCost);
							if(canBuy) item.button.interactable = true;
							else item.button.interactable = false;
							break;
						}
					}
			}

			foreach(UISelectItem item in abilityItemList){
				if(item.label.text == AbilityManager.GetSelectedAbility().name){
					item.selectHighlight.SetActive(true);
					item.button.interactable = false;
					break;
				}
			}

			UnitPlayer player=GameControl.GetPlayer();
			List<string> boughtWeapons = _shop.BoughtWeapon();
			UIItemSelectFilter(ref weaponItemList,ref boughtWeapons);

			List<Weapon> weapons = new(player.weaponList);
			foreach(UISelectItem item in weaponItemList){
			foreach(Weapon weapon in weapons){
						if(weapon.weaponName == item.label.text & !boughtWeapons.Contains(weapon.weaponName)){
							item.buttonText.text = weapon.ShopCost.ToString();
							canBuy = _shop.CanBuy(weapon.ShopCost);
							if(canBuy) item.button.interactable = true;
							else item.button.interactable = false;
							break;
						}
					}
			}

			foreach(UISelectItem item in weaponItemList){
				if(item.label.text == player.weaponList[player.GetCurrentWeaponIndex()].weaponName){
					item.selectHighlight.SetActive(true);
					item.button.interactable = false;
					break;
				}
			}
			// for(int i=0; i<player.weaponList.Count; i++){
			// 	string clip=player.weaponList[i].currentClip<0 ? "∞" : player.weaponList[i].currentClip.ToString();
			// 	string ammo=player.weaponList[i].ammo<0 ? "∞" : player.weaponList[i].ammo.ToString();
				
			// 	weaponItemList[i].labelAlt.text=clip+"/"+ammo;
				
			// 	if(i==player.weaponID){
			// 		weaponItemList[i].selectHighlight.SetActive(true);
			// 		weaponItemList[i].button.interactable=false;
					
			// 	}
			// 	else{
			// 		weaponItemList[i].selectHighlight.SetActive(false);
			// 		weaponItemList[i].button.interactable=true;
			// 	}
			// }


		}
		private void UIItemSelectFilter(ref List<UISelectItem> uISelectItems,ref List<string> boughtItem){
			ColorBlock colorBlock;
			foreach(UISelectItem item in uISelectItems){
				if(boughtItem.Contains(item.label.text)){
					item.buttonText.text = "Select";
					colorBlock = item.button.colors;
					colorBlock.normalColor = Color.white;
					item.button.colors = colorBlock;
					item.selectHighlight.SetActive(false);
					item.button.interactable = true;
				}
				else{
					item.selectHighlight.SetActive(false);
					colorBlock = item.button.colors;
					colorBlock.normalColor = Color.green;
					item.button.colors = colorBlock;
				}
			}
		}
		public void OnSelectWeapon(GameObject butObj){
			int newID=0;
			for(int i=0; i<weaponItemList.Count; i++){
				if(butObj==weaponItemList[i].buttonObj){
					newID=i;	break;
				}
			}
			
			int weaponID=GameControl.GetPlayer().weaponID;
			if(newID==weaponID) return;
			
			Weapon weapon = WeaponDB.GetPrefab1(newID);
			
			if(_shop.HasBoughtWeapon(weapon.weaponName)){
				ActivateButton();
				return;
			}

			if(_shop.BuyWeapon(weapon.ShopCost,weapon.weaponName)){
				ActivateButton();
				UpdateTab();
				return;
			}
			
			void ActivateButton(){
				weaponItemList[newID].selectHighlight.SetActive(true);
				weaponItemList[newID].button.interactable=false;
			
				weaponItemList[weaponID].selectHighlight.SetActive(false);
				weaponItemList[weaponID].button.interactable=true;
			
				GameControl.GetPlayer().SwitchWeapon(newID);
			}
			
		}
		public void OnSelectAbility(GameObject butObj){
			int newID=0;
			for(int i=0; i<abilityItemList.Count; i++){
				if(butObj==abilityItemList[i].buttonObj){
					newID=i;	break;
				}
			}
			
			int abilityID=AbilityManager.GetSelectID();
			if(newID==abilityID) return;
			
			Ability ability = AbilityDB.CloneItem1(newID);

			if(_shop.HasBoughtAbility(ability.name)){
				ActivateButton();
				return;
			}

			if(_shop.BuyAbility(ability.ShopCost,ability.name)){
				ActivateButton();
				UpdateTab();
				return;
			}
			
			void ActivateButton(){
				abilityItemList[newID].selectHighlight.SetActive(true);
				abilityItemList[newID].button.interactable=false;
			
				abilityItemList[abilityID].selectHighlight.SetActive(false);
				abilityItemList[abilityID].button.interactable=true;
			
				AbilityManager.Select(newID);
			}
		}
		
		
		void Update () {
			if(Input.GetKeyDown(KeyCode.Tab)){
				_TurnTabOn();
			}
			else if(Input.GetKeyUp(KeyCode.Tab)){
				_TurnTabOff();
			}
		}
		
		
		public static void TurnTabOn(){ instance._TurnTabOn(); }
		public void _TurnTabOn(){
			Time.timeScale=0;
			Cursor.visible=true;
			
			isOn=true;
			
			TDSTouchInput.Hide();
			
			GameControl.GetPlayer().DisableFire();
			UpdateTab();
			tabObject.SetActive(true);
		}
		public static void TurnTabOff(){ instance._TurnTabOff(); }
		public void _TurnTabOff(){
			Time.timeScale=1;
			Cursor.visible=false;
			
			isOn=false;
			
			TDSTouchInput.Show();
			
			GameControl.GetPlayer().EnableFire();
			
			tabObject.SetActive(false);
		}
		
		
		public void OnCloseButton(){
			_TurnTabOff();
		}
		
		
		private bool isOn=false;
		public static bool IsOn(){ return instance.isOn; }
		
	}

}