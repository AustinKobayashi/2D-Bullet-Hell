//using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerWizardStatsTest : AbstractPlayerStats {

	float regenTimer;
	//InventoryControls controls; shouldnt be necessary due to the stat hooks (requires testing)

	// Use this for initialization
	void Start () {
		//controls = GetComponent<InventoryControls> (); shouldnt be necessary due to the stat hooks (requires testing)

		// Used for testing
		SetHealth (10000);
		maxHealth = 100;
		SetStrength (12);
		SetDexterity (15);
		endurance = 12;
		SetDefence (0);
	}
	
	// Update is called once per frame
	// Regenerate health and mana every second
	void Update () {
		regenTimer += Time.deltaTime;
		if(regenTimer >= 1){
			regenTimer = 0;
			CmdRegenHealth ();
			CmdRegenMana ();
		}
	}

	[Command]
	void CmdRegenHealth(){
		health += (1 + (int)(0.15f * endurance));
		if (health > maxHealth)
			health = maxHealth;

		//controls.UpdateStatText (); shouldnt be necessary due to the stat hooks (requires testing)
	}

	[Command]
	void CmdRegenMana(){
		mana += (1 + (int)(0.15f * wisdom));
		if (mana > maxMana)
			mana = maxMana;
		
		//controls.UpdateStatText (); shouldnt be necessary due to the stat hooks (requires testing)
	}
		
	// Increases each stat by a random value
	[Command]
	public override void CmdLevelUp(){
		level++;
		maxHealth += Random.Range (20, 31);
		mana += Random.Range (5, 16);
		strength += Random.Range (1, 3);
		speed += Random.Range (0, 3);
		dexterity += Random.Range (1, 3);
		endurance += Random.Range (0, 2);
		wisdom += Random.Range (0, 3);
		//controls.UpdateStatText (); shouldnt be necessary due to the stat hooks (requires testing)
	}
}
