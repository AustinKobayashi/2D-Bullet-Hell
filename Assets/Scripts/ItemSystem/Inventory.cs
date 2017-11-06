using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {

	//Synclist representing the players inventory.
	public SyncListInt inventory = new SyncListInt();
	ItemDatabase database;
	//[SyncVar]s for the players equiped items.
	[SyncVar] private int weapon;
	[SyncVar] private int ability;
	[SyncVar] private int armour;
	[SyncVar] private int ring;

	// Sets all inventory slots to -1 (as 0 is an item).
	void Start () {
		weapon = -1;
		ability = -1;
		armour = -1;
		ring = -1;
		weapon = new StaffTest ().GetId();
        ability = new ScrollTest().GetId();

		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
		Debug.Log (database);
		for (int i = 0; i < 8; i++)
			inventory.Add (-1);
		
		inventory [0] = new ArmourTest ().GetId();
		inventory [1] = new SwordTest ().GetId();
		inventory [2] = new PotionTest ().GetId();
		inventory [3] = new ScrollTest ().GetId();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Adds item to the inventory as long as its not full.
	[Command]
	public void CmdAddItem(Item item){
		if(!InventoryIsFull())
			inventory [GetFirstFreeSlot ()] = item.GetId();
	}

	/*
	[Command]
	public void CmdRemoveItem(Item item){
		for (int i = 0; i < 8; i++)
			if (inventory [i] == item)
				inventory [i] = null;
	}*/
	
	//removes an item from the inventory at index index.
	[Command]
	public void CmdRemoveItem(int index){
		inventory [index] = -1;
	}

	//Returns the item at index in the players inventory.
	[Server]
	public Item GetItem(int index){
		return database.GetItem(inventory [index]);
	}
		

	/*
	[Command]
	public void CmdEquipItem(Item item){
		weapon = item;
		CmdRemoveItem (item);
	}
	*/

	/* Equips the weapon
	 * - If the index = -1 this means that a weapon is selected in the inventory
	 *
	 *
	 */
	//Sets a players weapon to an item in their inventory at index (if it is a weapon).
	[Command]
	public void CmdEquipWeapon(int index){

		if (GetItem (index).GetItemType () != ItemTypes.weapon)
			return;
		
		if (inventory [index] == -1) {
			weapon = GetItem (index).GetId ();
			CmdRemoveItem (index);
		} else {
			int temp = weapon;
			weapon = GetItem (index).GetId();
			inventory [index] = temp;
		}
	}
	//Sets a players Ability to an item in their inventory at index (if it is an Ability).
	[Command]
	public void CmdEquipAbility(int index){

		if (GetItem (index).GetItemType () != ItemTypes.ability)
			return;
		if (inventory [index] == -1) {
			ability = GetItem (index).GetId ();
			CmdRemoveItem (index);
		} else {
			int temp = ability;
			ability = GetItem (index).GetId();
			inventory [index] = temp;
		}
	}
	//Sets a players Armour to an item in their inventory at index (if it is an Armour).
	[Command]
	public void CmdEquipArmour(int index){

		if (GetItem (index).GetItemType () != ItemTypes.armour)
			return;
		if (inventory [index] == -1) {
			armour = GetItem (index).GetId ();
			CmdRemoveItem (index);
		} else {
			int temp = armour;
			armour = GetItem (index).GetId();
			inventory [index] = temp;
		}
	}
	//Unequips an item and moves it back into the players inventory.	
	[Command]
	public void CmdUnequipItem(int selectedItem, int index){
		ItemTypes type = database.GetItem (selectedItem).GetItemType ();
		switch(type){
		case(ItemTypes.weapon):
			if(inventory[index] == -1){
				inventory [index] = weapon;
				weapon = -1;
			} else {
				CmdEquipWeapon (index);
			}
			break;
		case(ItemTypes.ability):
			if(inventory[index] == -1){
				inventory [index] = ability;
				ability = -1;
			} else {
				CmdEquipAbility (index);
			}
			break;
		case(ItemTypes.armour):
			if(inventory[index] == -1){
				inventory [index] = armour;
				armour = -1;
			} else {
				CmdEquipArmour (index);
			}
			break;
		default:
			break;
		}
	}
	//Swaps two items by index in the inventory.
	[Command]
	public void CmdSwapItems(int index1, int index2){
		Item temp = GetItem (index1);
		inventory [index1] = inventory [index2];
		inventory [index2] = temp.GetId();
	}
	
	//Returns the players currently equipped weapon/ability/armour.
	[Server]
	public Weapon GetWeapon(){
		return database.GetItem(weapon) as Weapon;	
	}


	[Server]
	public AbilityItem GetAbility(){
		return database.GetItem(ability) as AbilityItem;	
	}

	[Server]
	public Armour GetArmour(){
		return database.GetItem(armour) as Armour;	
	}
		
	//returns the first free inventory slot or -1 if there are no free slots.
	int GetFirstFreeSlot(){
		for (int i = 0; i < 8; i++)
			if (inventory [i] == -1)
				return i;

		return -1;
	}

	
	//Returns true if inventory is full, false otherwise;
	bool InventoryIsFull(){
		for (int i = 0; i < 8; i++)
			if (inventory [i] == -1)
				return false;
		return true;
	}
}
