using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyStatsTest : AbstractStats {

	[SyncVar] float experienceMultiplier;

	// Use this for initialization
	void Start () {
		SetHealth (1000);
		maxHealth = health;
		SetStrength (5);
		SetDefence (0);
		experienceMultiplier = 2f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int GetExperienceGain(){
		return (int)((maxHealth / 10) * experienceMultiplier);
	}
}
