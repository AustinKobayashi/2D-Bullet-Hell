using System;
using System.Collections;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

// Base class for all player stats
public abstract class AbstractPlayerStats : AbstractStats {

	[SyncVar] protected int mana;
	[SyncVar] protected int endurance;
	[SyncVar] protected int wisdom;
	[SyncVar] protected int experience;
	[SyncVar] protected int level;
	[SyncVar] protected int maxMana;
	[SyncVar] protected int experienceToLevel;
	[SyncVar] protected string PlayerName;
	Inventory inventory;

	void Awake(){
		level = 0; // for testing, need to be removed since this will be called everytime a player joins a room
		if (level == 0)
			experienceToLevel = 50;

		inventory = GetComponent<Inventory> ();
	}

	// Returns the weapon damage or 0 if no weapon is equipped
	public int GetWpnDamage(){
		try{
			return Random.Range (inventory.GetWeapon().GetDamage()[0], inventory.GetWeapon().GetDamage()[1] + 1);
		}catch(System.NullReferenceException){
			return 0;
		}
	}

	// Returns the ability power or 0 if no ability is equipped
	public int GetAbilityPower(){
		try{
			return Random.Range (inventory.GetAbility().GetDamage()[0], inventory.GetAbility().GetDamage()[1] + 1);
		}catch(System.NullReferenceException){
			return 0;
		}
	}

	// Called when the player kills an enemy
	public void IncreaseExperience(int amount){
		experience += amount;
		while (experience >= experienceToLevel) {
			LevelUp ();
			experience -= experienceToLevel;
			experienceToLevel += 100;
		}

		//controls.UpdateStatText (); shouldnt be necessary due to the stat hooks (requires testing)
	}

	public virtual void LevelUp(){
	}

	#region stats getters and setters
	public int GetMana(){
        return mana;
    }

	public void SetMana(int mana){
		this.mana = mana;
	}

	public int GetMaxMana() {
		return maxMana;
	}

	public int GetEndurance(){
        return endurance;
    }

	public void SetEndurance(int endurance){
		this.endurance = endurance;
	}

	public int GetWisdom(){
        return wisdom;
    }

	public void SetWisdom(int wisdom){
		this.wisdom = wisdom;
	}

	public int GetLevel(){
		return level;
	}

	public void SetLevel(int level){
		this.level = level;
	}

	public int GetExperience(){
		return experience;
	}

	public void SetExperience(int experience){
		this.experience = experience;
	}

	public int GetMaxExperience() {
		return experienceToLevel;
	}

	public String GetPlayerName() {
		return PlayerName;
	}

	public void SetPlayerName(String playername) {
		PlayerName = playername;
	}
	#endregion

}
