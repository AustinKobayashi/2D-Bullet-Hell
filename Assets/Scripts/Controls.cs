using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Controls : NetworkBehaviour {

	//public Image menu;
	public GameObject menu;
	public GameObject statsMenu;
	public GameObject inventoryMenu;
	Inventory inventory;
	AbstractPlayerStats stats;
	public Text healthValue;
	public Text manaValue;
	public Text levelValue;
	public Text experienceValue;
	public Text strengthValue;
	public Text defenceValue;
	public Text speedValue;
	public Text dexterityValue;
	public Text enduranceValue;
	public Text wisdomValue;
	[SyncVar] int selectedItemIndex;
	[SyncVar] Item selectedItem;
	public Image weaponSlot, abilitySlot, armorSlot, ringSlot, inventorySlot0, inventorySlot1, inventorySlot2, inventorySlot3, inventorySlot4, inventorySlot5,
	inventorySlot6, inventorySlot7;
	public Sprite defautImg;

	// Use this for initialization
	void Start () {
		menu.SetActive(false);
		stats = GetComponent<AbstractPlayerStats> ();
		inventoryMenu.SetActive (false);
		statsMenu.SetActive (true);
		inventory = GetComponent<Inventory> ();
		selectedItemIndex = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) 
			ToggleMenu ();
	}


	void ToggleMenu(){
		menu.SetActive (!menu.activeInHierarchy);
		UpdateStatText ();
	}

	public void UpdateStatText(){
		healthValue.text = stats.GetHealth ().ToString();
		manaValue.text = stats.GetMana ().ToString();
		levelValue.text = stats.GetLevel ().ToString();
		experienceValue.text = stats.GetExperience ().ToString();
		strengthValue.text = stats.GetStrength ().ToString();
		defenceValue.text = stats.GetDefence ().ToString();
		speedValue.text = stats.GetSpeed ().ToString();
		dexterityValue.text = stats.GetDexterity ().ToString();
		enduranceValue.text = stats.GetEndurance ().ToString();
		wisdomValue.text = stats.GetWisdom ().ToString();
	}

	public void SwitchToStatsMenu(){
		statsMenu.SetActive (true);
		inventoryMenu.SetActive (false);
	}

	public void SwitchToInventoryMenu(){
		statsMenu.SetActive (false);
		inventoryMenu.SetActive (true);
		LoadItemImages ();
	}
		
	public void LoadItemImages(){

		weaponSlot.sprite = inventory.GetWeapon() != null ? inventory.GetWeapon().GetItemImage () : defautImg;
		inventorySlot0.sprite = inventory.GetItem (0) != null ? inventory.GetItem (0).GetItemImage () : defautImg;
		inventorySlot1.sprite = inventory.GetItem (1) != null ? inventory.GetItem (1).GetItemImage () : defautImg;
		inventorySlot2.sprite = inventory.GetItem (2) != null ? inventory.GetItem (2).GetItemImage () : defautImg;
		inventorySlot3.sprite = inventory.GetItem (3) != null ? inventory.GetItem (3).GetItemImage () : defautImg;
		inventorySlot4.sprite = inventory.GetItem (4) != null ? inventory.GetItem (4).GetItemImage () : defautImg;
		inventorySlot5.sprite = inventory.GetItem (5) != null ? inventory.GetItem (5).GetItemImage () : defautImg;
		inventorySlot6.sprite = inventory.GetItem (6) != null ? inventory.GetItem (6).GetItemImage () : defautImg;
		inventorySlot7.sprite = inventory.GetItem (7) != null ? inventory.GetItem (7).GetItemImage () : defautImg;

	}

	public void ClickItemSlot(int index){

		if (selectedItemIndex == -1 && selectedItem == null) { 
			if(inventory.GetItem(index) != null)
				selectedItemIndex = index;
		}else{
			if (selectedItem == null) {
				inventory.CmdSwapItems (selectedItemIndex, index);
				selectedItemIndex = -1;
			}else {
				inventory.CmdUnequipWeapon (index);
				selectedItem = null;
			}
		}
		LoadItemImages ();
	}

	public void ClickWeaponSlot(){
		if (selectedItemIndex != -1) {
			inventory.CmdEquipItem (selectedItemIndex);
			LoadItemImages ();
		} else {
			Debug.Log ("No selected item");
			selectedItem = inventory.GetWeapon ();
		}
	}
}
