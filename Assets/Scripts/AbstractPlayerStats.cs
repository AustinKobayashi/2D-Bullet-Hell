using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class AbstractPlayerStats : AbstractStats {

	[SyncVar] protected int mana;
	[SyncVar] protected int maxMana;
	[SyncVar] protected int endurance;
	[SyncVar] protected int wisdom;
	[SyncVar] protected int experience;
	[SyncVar] protected int experienceToLevel;
	[SyncVar] protected int level;

	void Start(){
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
}
