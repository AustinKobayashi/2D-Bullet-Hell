using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {
	
	//Synclist representing the players inventory.
	ItemDatabase _database;
	private InventoryHandler _inventoryHandler;
	private int _inventorySize = 5;
	private int _inventoryOffset = 5;
	//[SyncVar]s for the players equiped items.
	[SyncVar] private int _weapon;
	[SyncVar] private int _ability;
	[SyncVar] private int _armour;
	[SyncVar] private int _ring;
	private SyncListInt _inventory = new SyncListInt();

	// Sets all inventory slots to -1 (as 0 is an item).
	void Start () {
		_weapon = -1;
		_ability = -1;
		_armour = -1;
		_ring = -1;
		_weapon = new StaffTest ().GetId();
        _ability = new ScrollTest().GetId();
		_database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
		_inventoryHandler = GetComponent<InventoryHandler>();
		for (int i = 0; i < _inventorySize; i++)
			_inventory.Add(-1);
		
		_inventory [0] = new ArmourTest ().GetId();
		_inventory [1] = new SwordTest ().GetId();
		_inventory [2] = new PotionTest ().GetId();
		_inventory [3] = new ScrollTest ().GetId();
	}

	public void UpdateUI() {
		Item[] inv = new Item[5];
		for (int i = 0; i < _inventorySize; i++) {
			inv[i] = GetItem(i);
		}
		gameObject.GetComponent<StatsHandler>().UpdateText();
		_inventoryHandler.UpdateSlots(GetWeapon(), GetAbility(), GetArmour(), null, inv);
	}

	private IEnumerator updateOffset() {
		yield return new WaitForSeconds(0.10f);
		UpdateUI();
		yield return null;
	}

	public void Swap(int firstSelection, int secondSelection) {
		if (!isLocalPlayer) {
			return;
		}
		if (firstSelection < 5 && secondSelection < 5) return;
		if (firstSelection < 5) {
			SwitchEquipment(firstSelection, secondSelection-_inventoryOffset);
		} else if (secondSelection < 5) {
			SwitchEquipment(secondSelection, firstSelection-_inventoryOffset);
		} else {
			CmdSwapItems(firstSelection - _inventoryOffset, secondSelection - _inventoryOffset);
		}
		StartCoroutine(updateOffset());
	}

	private void SwitchEquipment(int equipment, int index) {
		switch (equipment) {
				case 1:
					CmdEquipWeapon(index);
					break;
				case 2:
					CmdEquipAbility(index);
					break;
				case 3:
					CmdEquipArmour(index);
					break;
				case 4:
					//TODO: Implement with Ring
					break;
		}
	}

	//Adds item to the inventory as long as its not full.
	[Command]
	public void CmdAddItem(Item item){
		if(!InventoryIsFull())
			_inventory [GetFirstFreeSlot ()] = item.GetId();
	}
	
	//removes an item from the inventory at index index.
	[Command]
	public void CmdRemoveItem(int index){
		_inventory [index] = -1;
	}

	//Returns the item at index in the players inventory.
	public Item GetItem(int index){
		return _database.GetItem(_inventory [index]);
	}

	/* Equips the weapon
	 * - If the index = -1 this means that a weapon is selected in the inventory
	 *
	 *
	 */
	//Sets a players weapon to an item in their inventory at index (if it is a weapon).
	[Command]
	public void CmdEquipWeapon(int index){
		if (_inventory[index] == -1) {
			_inventory[index] = _weapon;
			_weapon = -1;
			return;
		}
		if (GetItem (index).GetItemType () != ItemTypes.weapon)
			return;
		if (_weapon == -1) {
			_weapon = GetItem (index).GetId ();
			CmdRemoveItem (index);
		} else {
			int temp = _weapon;
			_weapon = GetItem (index).GetId();
			_inventory [index] = temp;
		}
	}
	//Sets a players Ability to an item in their inventory at index (if it is an Ability).
	[Command]
	public void CmdEquipAbility(int index){
		if (_inventory[index] == -1) {
			_inventory[index] = _ability;
			_ability = -1;
			return;
		}
		if (GetItem (index).GetItemType () != ItemTypes.ability)
			return;
		if (_ability == -1) {
			_ability = GetItem (index).GetId ();
			CmdRemoveItem (index);
		} else {
			int temp = _ability;
			_ability = GetItem (index).GetId();
			_inventory [index] = temp;
		}
	}
	//Sets a players Armour to an item in their inventory at index (if it is an Armour).
	[Command]
	public void CmdEquipArmour(int index){
		if (_inventory[index] == -1) {
			_inventory[index] = _armour;
			_armour = -1;
			return;
		}
		if (GetItem (index).GetItemType () != ItemTypes.armour)
			return;
		if (_armour == -1) {
			_armour = GetItem (index).GetId ();
			CmdRemoveItem (index);
		} else {
			int temp = _armour;
			_armour = GetItem (index).GetId();
			_inventory [index] = temp;
		}
	}
	//Swaps two items by index in the inventory.
	[Command]
	public void CmdSwapItems(int index1, int index2){
		Item temp = GetItem (index1);
		_inventory [index1] = _inventory [index2];
		_inventory [index2] = temp.GetId();
	}
	//Returns the players currently equipped weapon/ability/armour.
	public Weapon GetWeapon(){
		return _database.GetItem(_weapon) as Weapon;	
	}

	public AbilityItem GetAbility(){
		return _database.GetItem(_ability) as AbilityItem;	
	}

	public Armour GetArmour(){
		return _database.GetItem(_armour) as Armour;	
	}
		
	//returns the first free inventory slot or -1 if there are no free slots.
	private int GetFirstFreeSlot(){
		for (int i = 0; i < 8; i++)
			if (_inventory [i] == -1)
				return i;
		return -1;
	}
	//Returns true if inventory is full, false otherwise;
	private bool InventoryIsFull(){
		for (int i = 0; i < 8; i++)
			if (_inventory [i] == -1)
				return false;
		return true;
	}
}
