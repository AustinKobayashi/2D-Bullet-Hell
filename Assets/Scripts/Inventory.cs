using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {

	//public Item[] inventory = new Item[8];
	SyncListInt inventory = new SyncListInt();
	ItemDatabase database;
	[SyncVar] private Item weapon;

	// Use this for initialization
	void Start () {
		weapon = new StaffTest ();
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
	[Command]
	public void CmdEquipItem(int index){
		if (inventory [index] == -1) {
			weapon = GetItem (index);
			CmdRemoveItem (index);
		} else {
			Item temp = weapon;
			weapon = GetItem (index);
			inventory [index] = temp.GetId ();
		}
	}

	[Command]
	public void CmdUnequipWeapon(int index){
		if(inventory[index] == -1){
			inventory [index] = weapon.GetId ();
			weapon = null;
		} else {
			CmdEquipItem (index);
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
		return weapon as Weapon;	
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
