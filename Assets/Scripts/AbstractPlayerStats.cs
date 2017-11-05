using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class AbstractPlayerStats : AbstractStats {

	[SyncVar (hook = "UpdateManaText")] protected int mana;
	[SyncVar] protected int maxMana;
	[SyncVar (hook = "UpdateEnduranceText")] protected int endurance;
	[SyncVar (hook = "UpdateWisdomText")] protected int wisdom;
	[SyncVar(hook = "UpdateExperienceText") ] protected int experience;
	[SyncVar] protected int experienceToLevel;
	[SyncVar (hook = "UpdateLevelText")] protected int level;
	InventoryControls inventoryControls;

	void Start(){
		inventoryControls = GetComponent<InventoryControls> ();
		level = 0;
		if (level == 0)
			experienceToLevel = 50;
	}

	void Update () {

	}

	public int GetMana(){
        return mana;
    }

	public int GetEndurance(){
        return endurance;
    }

	public int GetWisdom(){
        return wisdom;
    }

	public int GetLevel(){
		return level;
	}

	public int GetExperience(){
		return experience;
	}
		

	public void UpdateManaText(int mana){
		if (!isLocalPlayer)
			return;
		inventoryControls.UpdateManaText (mana);
	}

	public void UpdateEnduranceText(int endurance){
		if (!isLocalPlayer)
			return;
	}

	public void UpdateWisdom(int wisdom){
		if (!isLocalPlayer)
			return;
	}

	public void UpdateExperienceText(int experience){
		if (!isLocalPlayer)
			return;
	}

	public void UpdateLeveText(int level){
		if (!isLocalPlayer)
			return;
	}
}
