using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// Base class for all player stats
public abstract class AbstractPlayerStats : AbstractStats {

	[SyncVar (hook = "UpdateManaText")] protected int mana;
	[SyncVar (hook = "UpdateEnduranceText")] protected int endurance;
	[SyncVar (hook = "UpdateWisdomText")] protected int wisdom;
	[SyncVar(hook = "UpdateExperienceText") ] protected int experience;
	[SyncVar (hook = "UpdateLevelText")] protected int level;
	[SyncVar] protected int maxMana;
	[SyncVar] protected int experienceToLevel;
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
			CmdLevelUp ();
			experience -= experienceToLevel;
			experienceToLevel += 100;
		}

		//controls.UpdateStatText (); shouldnt be necessary due to the stat hooks (requires testing)
	}

	[Command]
	public virtual void CmdLevelUp(){
	}

	#region stats getters and setters
	public int GetMana(){
        return mana;
    }

	public void SetMana(int mana){
		this.mana = mana;
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
	#endregion

	#region UpdateTexts
	public void UpdateManaText(int mana){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateManaText (mana);
	}

	public void UpdateEnduranceText(int endurance){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateEnduranceText (endurance);
	}

	public void UpdateWisdomText(int wisdom){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateWisdomText (wisdom);
	}

	public void UpdateExperienceText(int experience){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateExperienceText (experience);
	}

	public void UpdateLevelText(int level){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateLevelText (level);
	}
	#endregion
}
