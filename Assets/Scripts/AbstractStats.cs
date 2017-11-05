using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public abstract class AbstractStats : NetworkBehaviour {

	[SyncVar (hook = "UpdateManaText")] public int health;
	[SyncVar] public int maxHealth;
	[SyncVar (hook = "UpdateStrengthText")] public int strength;
	[SyncVar (hook = "UpdateDefenceText")] public int defence;
	[SyncVar (hook = "UpdateSpeedText")] public int speed;
	[SyncVar (hook = "UpdateDexterityText")] public int dexterity;
	InventoryControls inventoryControls;

   	// Use this for initialization
	void Start () {
		if (isLocalPlayer)
			inventoryControls = GetComponent<InventoryControls> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Die(){
		Destroy (this.gameObject);
	}

	public int GetHealth(){
		return health;
	}

	public void SetHealth(int health){
		this.health = health;
	}

	[Server]
	public bool TakeDamage(int damage){
		
		health -= (int)(damage * 0.15f);

		if (defence < (int)(damage * 0.85f)) {
			health -= ((int)(damage * 0.85f) - defence);

		}

		if (health <= 0) {
			Die ();
			return true;
		}
			
		return false;
	}

	public int GetStrength(){
		return strength;
	}

	public void SetStrength(int strength){
		this.strength = strength;
	}

	public int GetDefence(){
		return defence;
	}

	public void SetDefence(int defence){
		this.defence = defence;
	}

	public int GetSpeed(){
		return speed;
	}

	public int GetDexterity(){
		return dexterity;
	}

	public void SetDexterity(int dexterity){
		this.dexterity = dexterity;
	}

	[Command]
	public void CmdIncreaseDefence(int amount){
		this.defence += amount;
	}

	[Command]
	public void CmdDecreaseDefence(int amount){
		this.defence -= amount;
	}



	public void UpdateHealthText(int health){
		if (!isLocalPlayer)
			return;
	}

	public void UpdateStrengthText(int strength){
		if (!isLocalPlayer)
			return;
	}

	public void UpdateDefence(int defence){
		if (!isLocalPlayer)
			return;
	}

	public void UpdateSpeedText(int speed){
		if (!isLocalPlayer)
			return;
	}

	public void UpdateDexterityText(int dexterity){
		if (!isLocalPlayer)
			return;
	}
}
