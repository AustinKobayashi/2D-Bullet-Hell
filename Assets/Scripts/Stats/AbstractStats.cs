using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Base class for all stats
public abstract class AbstractStats : NetworkBehaviour {

	[SyncVar (hook = "UpdateHealthText")] public int health;
	[SyncVar (hook = "UpdateStrengthText")] protected int strength;
	[SyncVar (hook = "UpdateDefenceText")] protected int defence;
	[SyncVar (hook = "UpdateSpeedText")] protected int speed;
	[SyncVar (hook = "UpdateDexterityText")] protected int dexterity;
	[SyncVar] protected int maxHealth;
    public InventoryControls inventoryControls;

    protected bool invulnerable; 

	void Die(){
		Destroy (this.gameObject);
	}

	// Used by buffs
	[Command]
	public void CmdIncreaseDefence(int amount){
		this.defence += amount;
	}

	// Used by buffs
	[Command]
	public void CmdDecreaseDefence(int amount){
		this.defence -= amount;
	}


    [Command]
    public void CmdSetInvulnerable(bool invulnerable){
        this.invulnerable = invulnerable;
    }
		
	// up to 85% of damage can be reduced by armour
	// Returns true if the attack killed the unit
	[Server]
	public bool TakeDamage(int damage){

        if (invulnerable)
            return false;
        
		health -= (int)(damage * 0.15f);

		if (defence < (int)(damage * 0.85f))
			health -= ((int)(damage * 0.85f) - defence);

		if (health <= 0) {
			Die ();
			return true;
		}

		return false;
	}
		

	#region stats getters and setters
	public int GetHealth(){
		return health;
	}

	public void SetHealth(int health){
		this.health = health;
	}

	public int getMaxHealth()
	{
		return maxHealth;
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

	public void SetSpeed(int speed){
		this.speed = speed;
	}

	public int GetDexterity(){
		return dexterity;
	}

	public void SetDexterity(int dexterity){
		this.dexterity = dexterity;
	}

	#endregion

	// Syncvar hooks to update the text for the player menu
	#region UpdateTexts
	public void UpdateHealthText(int health){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateHealthText (health);
	}

	public void UpdateStrengthText(int strength){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateStrengthText (strength);
	}

	public void UpdateDefenceText(int defence){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateDefenceText (defence);
	}

	public void UpdateSpeedText(int speed){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateSpeedText (speed);
	}

	public void UpdateDexterityText(int dexterity){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateDexterityText (dexterity);
	}
	#endregion
}
