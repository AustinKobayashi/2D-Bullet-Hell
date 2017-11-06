using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyStatsTest : AbstractStats {

	[SyncVar] float experienceMultiplier;

	// Use this for initialization
	void Start () {

		// For testing
		SetHealth (1000);
		maxHealth = health;
		SetStrength (5);
		SetDefence (0);
		experienceMultiplier = 2f;
	}

	// Called when the player kills an enemy to calculate experience gain
	public int GetExperienceGain(){
		return (int)((maxHealth / 10) * experienceMultiplier);
	}
}
