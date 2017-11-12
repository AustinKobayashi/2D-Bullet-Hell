using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryHandler : NetworkBehaviour {
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
	private Button _firstButton;
	private Button _secondButton;

	private Canvas _invCanvas;

	public bool MenuOpen;
	// Use this for initialization
	void Start () {
		foreach (var button in GetComponentsInChildren<Button>()) {
			switch (button.gameObject.tag) {
				case "WeaponSlot":
					_weaponSlot = button;
					break;
				case "AbilitySlot":
					_abilitySlot = button;
					break;
				case "ArmourSlot":
					_armourSlot = button;
					break;
				case "RingSlot":
					_ringSlot = button;
					break;
				case "ItemSlot":
					_itemSlot1 = button;
					break;
				case "ItemSlot2":
					_itemSlot2 = button;
					break;
				case "ItemSlot3":
					_itemSlot3 = button;
					break;
				case "ItemSlot4":
					_itemSlot4 = button;
					break;
				case "ItemSlot5":
					_itemSlot5 = button;
					break;
			}
		}
		foreach (Canvas c in GetComponentsInChildren<Canvas>()) {
			if (!c.gameObject.CompareTag("Inventory")) continue;
			_invCanvas = c;
			break;
		}
		
	}

	public void UpdateSlots(Item weapon, Item ability, Item armour, Item ring, Item[] inventory) {
		if (weapon != null) {
			ActivateColor(_weaponSlot);
			_weaponSlot.image.sprite = weapon.GetItemImage();
		}
		else {
			DeactivateColor(_weaponSlot);
		}
		if (ability != null) {
			ActivateColor(_abilitySlot);
			_abilitySlot.image.sprite = ability.GetItemImage();
		}
		else {
			DeactivateColor(_abilitySlot);
		}
		if (armour != null) {
			ActivateColor(_armourSlot);
			_armourSlot.image.sprite = armour.GetItemImage();
		}
		else {
			DeactivateColor(_armourSlot);
		}
		if (ring != null) {
			ActivateColor(_ringSlot);
			_ringSlot.image.sprite = ring.GetItemImage();
		}
		else {
			DeactivateColor(_ringSlot);
		}
		if (inventory[0] != null) {
			ActivateColor(_itemSlot1);
			_itemSlot1.image.sprite = inventory[0].GetItemImage();
		}
		else {
			DeactivateColor(_itemSlot1);
		}
		if (inventory[1] != null) {
			ActivateColor(_itemSlot2);
			_itemSlot2.image.sprite = inventory[1].GetItemImage();
		}
		else {
			DeactivateColor(_itemSlot2);
		}
		if (inventory[2] != null) {
			ActivateColor(_itemSlot3);
			_itemSlot3.image.sprite = inventory[2].GetItemImage();
		}
		else {
			DeactivateColor(_itemSlot3);
		}
		if (inventory[3] != null) {
			ActivateColor(_itemSlot4);
			_itemSlot4.image.sprite = inventory[3].GetItemImage();
		}
		else {
			DeactivateColor(_itemSlot4);
		}
		if (inventory[4] != null) {
			ActivateColor(_itemSlot5);
			_itemSlot5.image.sprite = inventory[4].GetItemImage();
		}
		else {
			DeactivateColor(_itemSlot5);
		}
	}

	private void ActivateColor(Button b) {
		var tempColor = Color.white;
		tempColor.a = 255;
		b.image.color = tempColor;
	}
	private void DeactivateColor(Button b) {
		var tempColor = Color.white;
		tempColor.a = 0;
		b.image.sprite = null;
		b.image.color = tempColor;
	}


	private void selectButton(Button b) {
		Color c = Color.red;
		c.a = 0.8f;
		b.image.color = c;
	}
	public void ClickItem(int selected) {
		if (!isLocalPlayer) return;
		if (_firstSelection == 0) {
			if (gameObject.GetComponentInParent<Inventory>().checkEmpty(selected)) return;
			_firstSelection = selected;
			_firstButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
			selectButton(_firstButton);
			return;
		}
		if (selected == _firstSelection) {
			_firstSelection = 0;
			gameObject.GetComponentInParent<Inventory>().UpdateUI();
			return;
		}
		if (_firstSelection != 0) {
			_secondSelection = selected;
			_secondButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
			selectButton(_secondButton);
			GetComponentInParent<Inventory>().Swap(_firstSelection, _secondSelection);
			_secondSelection = 0;
			_firstSelection = 0;
			return;
		}
	}
	
	// Update is called once per frame
	private float _timer;
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			if (!isLocalPlayer) return;
			gameObject.GetComponent<Inventory>().UpdateUI();
			_invCanvas.enabled = !_invCanvas.enabled;
			_firstSelection = 0;
			MenuOpen = !MenuOpen;
		}
	}
}
