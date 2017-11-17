using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyStatsTest : AbstractStats {

	[SyncVar] float experienceMultiplier;

	// Use this for initialization
	void Start () {
		if (isServer) {
			// For testing
			SetHealth (100);
			maxHealth = health;
			SetStrength (5);
			SetDefence (0);
			experienceMultiplier = 2f;
		}
		UpdateHealthText(health);
	}

	// Called when the player kills an enemy to calculate experience gain
	public int GetExperienceGain(){
		return (int)((maxHealth / 10) * experienceMultiplier);
	}
}
