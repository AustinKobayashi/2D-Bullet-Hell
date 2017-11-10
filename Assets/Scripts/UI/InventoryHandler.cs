using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {
	private Button
		_weaponSlot,
		_abilitySlot,
		_armourSlot,
		_ringSlot,
		_itemSlot1,
		_itemSlot2,
		_itemSlot3,
		_itemSlot4,
		_itemSlot5;
	
	/*
	 * Selection slots enum: (1-indexed)
	 * 0 = nothing
	 * 1 = weapon
	 * 2 = ability
	 * 3 = armour
	 * 4 = ring
	 * 5-9 = Inventory[slot - 5]
	 */
	private int _firstSelection;
	private int _secondSelection;
	
	// Use this for initialization
	void Start () {
		_weaponSlot = GameObject.FindGameObjectWithTag("WeaponSlot").GetComponent<Button>();
		_abilitySlot = GameObject.FindGameObjectWithTag("AbilitySlot").GetComponent<Button>();
		_armourSlot = GameObject.FindGameObjectWithTag("ArmourSlot").GetComponent<Button>();
		_ringSlot = GameObject.FindGameObjectWithTag("RingSlot").GetComponent<Button>();
		_itemSlot1 = GameObject.FindGameObjectWithTag("ItemSlot").GetComponent<Button>();
		_itemSlot2 = GameObject.FindGameObjectWithTag("ItemSlot2").GetComponent<Button>();
		_itemSlot3 = GameObject.FindGameObjectWithTag("ItemSlot3").GetComponent<Button>();
		_itemSlot4 = GameObject.FindGameObjectWithTag("ItemSlot4").GetComponent<Button>();
		_itemSlot5 = GameObject.FindGameObjectWithTag("ItemSlot5").GetComponent<Button>();
	}

	public void UpdateSlots(Item weapon, Item ability, Item armour, Item ring, Item[] inventory) {
		if (weapon != null) {
			Color tempColor = _weaponSlot.image.color;
			tempColor.a = 255;
			_weaponSlot.image.color = tempColor;
			_weaponSlot.image.sprite = weapon.GetItemImage();
		}
		else {
			Color tempColor = _weaponSlot.image.color;
			tempColor.a = 0;
			_weaponSlot.image.color = tempColor;
		}
		if (ability != null) {
			Color tempColor = _abilitySlot.image.color;
			tempColor.a = 255;
			_abilitySlot.image.color = tempColor;
			_abilitySlot.image.sprite = ability.GetItemImage();
		}
		else {
			Color tempColor = _abilitySlot.image.color;
			tempColor.a = 0;
			_abilitySlot.image.color = tempColor;
		}
		if (armour != null) {
			Color tempColor = _armourSlot.image.color;
			tempColor.a = 255;
			_armourSlot.image.color = tempColor;
			_armourSlot.image.sprite = armour.GetItemImage();
		}
		else {
			Color tempColor = _armourSlot.image.color;
			tempColor.a = 0;
			_armourSlot.image.color = tempColor;

		}
		if (ring != null) {
			Color tempColor = _ringSlot.image.color;
			tempColor.a = 255;
			_ringSlot.image.color = tempColor;
			_ringSlot.image.sprite = ring.GetItemImage();
		}
		else {
			Color tempColor = _ringSlot.image.color;
			tempColor.a = 0;
			_ringSlot.image.color = tempColor;
		}
		if (inventory[0] != null) {
			Color tempColor = _itemSlot1.image.color;
			tempColor.a = 255;
			_itemSlot1.image.color = tempColor;
			_itemSlot1.image.sprite = inventory[0].GetItemImage();
		}
		else {
			Color tempColor = _itemSlot1.image.color;
			tempColor.a = 0;
			_itemSlot1.image.color = tempColor;
		}
		if (inventory[1] != null) {
			Color tempColor = _itemSlot2.image.color;
			tempColor.a = 255;
			_itemSlot2.image.color = tempColor;
			_itemSlot2.image.sprite = inventory[1].GetItemImage();
		}
		else {
			Color tempColor = _itemSlot2.image.color;
			tempColor.a = 0;
			_itemSlot2.image.color = tempColor;
		}
		if (inventory[2] != null) {
			Color tempColor = _itemSlot3.image.color;
			tempColor.a = 255;
			_itemSlot3.image.color = tempColor;
			_itemSlot3.image.sprite = inventory[2].GetItemImage();
		}
		else {
			Color tempColor = _itemSlot3.image.color;
			tempColor.a = 0;
			_itemSlot3.image.color = tempColor;
		}
		if (inventory[3] != null) {
			Color tempColor = _itemSlot4.image.color;
			tempColor.a = 255;
			_itemSlot4.image.color = tempColor;
			_itemSlot4.image.sprite = inventory[3].GetItemImage();
		}
		else {
			Color tempColor = _itemSlot4.image.color;
			tempColor.a = 0;
			_itemSlot4.image.color = tempColor;
		}
		if (inventory[4] != null) {
			Color tempColor = _itemSlot5.image.color;
			tempColor.a = 255;
			_itemSlot5.image.color = tempColor;
			_itemSlot5.image.sprite = inventory[0].GetItemImage();
		}
		else {
			Color tempColor = _itemSlot5.image.color;
			tempColor.a = 0;
			_itemSlot5.image.color = tempColor;
		}
	}

	public void ClickItem(int selected) {
		if (selected == _firstSelection) {
			_firstSelection = 0;
			Debug.Log("First: " + _firstSelection);
			Debug.Log("Second: " + _secondSelection);
			return;
		}
		if (_firstSelection != 0) {
			_secondSelection = selected;
			GetComponentInParent<Inventory>().Swap(_firstSelection, _secondSelection);
			_secondSelection = 0;
			_firstSelection = 0;
			Debug.Log("First: " + _firstSelection);
			Debug.Log("Second: " + _secondSelection);
			return;
		}
		_firstSelection = selected;
		Debug.Log("First: " + _firstSelection);
		Debug.Log("Second: " + _secondSelection);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			gameObject.GetComponentInParent<Inventory>().UpdateUI();
			GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>().enabled = !GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>().enabled;
		}
	}
}
