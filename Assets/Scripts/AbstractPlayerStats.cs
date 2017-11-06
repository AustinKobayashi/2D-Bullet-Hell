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

	void Awake(){
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
