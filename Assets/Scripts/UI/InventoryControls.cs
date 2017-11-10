using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InventoryControls : NetworkBehaviour {
	
	
	/*
	 * This script doesn't work at all for networked behavior (so no inventory for Clients)
	 * TODO: Network this script and fix the Inveotry UI on the client.
	 */
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

	[SyncVar] public int selectedItemIndex;
	[SyncVar] public int selectedItem;
	ItemTypes itemType;

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
		selectedItem = -1;
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
		abilitySlot.sprite = inventory.GetAbility() != null ? inventory.GetAbility().GetItemImage () : defautImg;
		armorSlot.sprite = inventory.GetArmour() != null ? inventory.GetArmour().GetItemImage () : defautImg;
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

		if (selectedItemIndex == -1 && selectedItem == -1) { 
			if(inventory.GetItem(index) != null)
				selectedItemIndex = index;
		}else{
			if (selectedItem == -1) {
				inventory.CmdSwapItems (selectedItemIndex, index);
				selectedItemIndex = -1;
			}else {
				inventory.CmdUnequipItem(selectedItem, index);
				selectedItem = -1;
			}
		}
		LoadItemImages ();
	}

	public void ClickWeaponSlot(){
		if (selectedItemIndex != -1) {
			inventory.CmdEquipWeapon (selectedItemIndex);
			selectedItemIndex = -1;
			LoadItemImages ();
		} else {
			selectedItem = inventory.GetWeapon () != null ? inventory.GetWeapon ().GetId() : -1;
		}
	}


	public void ClickAbilitySlot(){
		if (selectedItemIndex != -1) {
			inventory.CmdEquipAbility (selectedItemIndex);
			selectedItemIndex = -1;
			LoadItemImages ();
		} else {
			selectedItem = inventory.GetAbility () != null ? inventory.GetAbility ().GetId() : -1;
		}
	}

	public void ClickArmourSlot(){
		if (selectedItemIndex != -1) {
			inventory.CmdEquipArmour (selectedItemIndex);
			selectedItemIndex = -1;
			LoadItemImages ();
		} else {
			selectedItem = inventory.GetArmour () != null ? inventory.GetArmour ().GetId() : -1;
		}
	}

	public void UpdateHealthText(int health){
		healthValue.text = stats.GetHealth ().ToString();
		levelValue.text = stats.GetLevel ().ToString();
		experienceValue.text = stats.GetExperience ().ToString();
		enduranceValue.text = stats.GetEndurance ().ToString();
		wisdomValue.text = stats.GetWisdom ().ToString();
	}

	#region UpdateTexts
	public void UpdateManaText(int mana){
		manaValue.text = mana.ToString();
	}

	public void UpdateStrengthText(int strength){
		strengthValue.text = strength.ToString();

	}

	public void UpdateDefenceText(int defence){
		defenceValue.text = defence.ToString();

	}

	public void UpdateSpeedText(int speed){
		speedValue.text = speed.ToString();

	}

	public void UpdateDexterityText(int dexterity){
		dexterityValue.text = dexterity.ToString();
	} 

	public void UpdateEnduranceText(int endurance){
		enduranceValue.text = endurance.ToString();

	}

	public void UpdateWisdomText(int wisdom){
		wisdomValue.text = wisdom.ToString();

	}

	public void UpdateExperienceText(int experience){
		experienceValue.text = experience.ToString();

	}

	public void UpdateLevelText(int level){
		levelValue.text = level.ToString();
	}
	#endregion
}
