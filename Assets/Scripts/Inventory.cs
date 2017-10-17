using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {

	//public Item[] inventory = new Item[8];
	SyncListInt inventory = new SyncListInt();
	ItemDatabase database;
	//[SyncVar] private Item weapon;
	[SyncVar] private int weapon;
	[SyncVar] private int ability;
	[SyncVar] private int armour;
	[SyncVar] private int ring;

	// Use this for initialization
	void Start () {
		weapon = new StaffTest ().GetId();
		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
		Debug.Log (database);
		for (int i = 0; i < 8; i++)
			inventory.Add (-1);
		
		inventory [0] = new ArmourTest ().GetId();
		inventory [1] = new SwordTest ().GetId();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


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

	[Command]
	public void CmdRemoveItem(int index){
		inventory [index] = -1;
	}

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
	[Command]
	public void CmdEquipWeapon(int index){

		if (GetItem (index).GetItemType () != ItemTypes.weapon)
			return;
		
		if (inventory [index] == -1) {
			Debug.Log ("called");
			weapon = GetItem (index).GetId ();
			CmdRemoveItem (index);
		} else {
			int temp = weapon;
			weapon = GetItem (index).GetId();
			inventory [index] = temp;
		}
	}

	[Command]
	public void CmdUnequipWeapon(int index){
		if(inventory[index] == -1){
			inventory [index] = weapon;
			weapon = -1;
		} else {
			CmdEquipWeapon (index);
		}
	}
	
	[Command]
	public void CmdEquipArmour(int index){

		if (GetItem (index).GetItemType () != ItemTypes.armour)
			return;

		if (inventory [index] == -1) {
			Debug.Log ("called");
			armour = GetItem (index).GetId ();
			CmdRemoveItem (index);
		} else {
			int temp = armour;
			weapon = GetItem (index).GetId();
			inventory [index] = temp;
		}
	}

	[Command]
	public void CmdUnequipArmour(int index){
		if(inventory[index] == -1){
			inventory [index] = armour;
			weapon = -1;
		} else {
			CmdEquipArmour (index);
		}
	}

	[Command]
	public void CmdSwapItems(int index1, int index2){
		Item temp = GetItem (index1);
		inventory [index1] = inventory [index2];
		inventory [index2] = temp.GetId();
	}

	[Server]
	public Weapon GetWeapon(){
		return database.GetItem(weapon) as Weapon;	
	}

	int GetFirstFreeSlot(){
		for (int i = 0; i < 8; i++)
			if (inventory [i] == null)
				return 1;

		return -1;
	}

	bool InventoryIsFull(){
		for (int i = 0; i < 8; i++)
			if (inventory [i] == null)
				return false;
		return true;
	}
}
