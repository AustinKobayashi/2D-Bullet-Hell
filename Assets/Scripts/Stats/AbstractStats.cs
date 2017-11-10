using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Base class for all stats
public abstract class AbstractStats : NetworkBehaviour {
/*
 * TODO: Stats needs to be looked at and redesigned for NetworkBehavior
 * Ideally more things would be initialized at start (for new clients joining in)
 * Also since I think there can only be one hook per syncvar, we need to make them count.
 */
	[SyncVar (hook = "UpdateHealthText")] public int health;
	[SyncVar] protected int strength;
	[SyncVar] protected int defence;
	[SyncVar] protected int speed;
	[SyncVar] protected int dexterity;
	[SyncVar] protected int maxHealth;
	public GameObject Bar;


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
	/*
	TODO: Set fill amount on start so its loaded without calling the hook
		(add start to this class, probably should ask austin first)
	*/
	public void UpdateHealthText(int health){
		Image i = Bar.GetComponent<Image>();
		var fillamt = health / (float) getMaxHealth();
		i.fillAmount = fillamt < 0 ? 0 : fillamt;
	}

	#endregion
}
