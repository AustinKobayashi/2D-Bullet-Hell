using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerWizardStatsTest : AbstractPlayerStats {

	float timer;
	Inventory inventory;
	Controls controls;

	// Use this for initialization
	void Start () {
		inventory = GetComponent<Inventory> ();
		controls = GetComponent<Controls> ();
		SetHealth (10000);
		maxHealth = 100;
		SetStrength (12000);
		SetDexterity (15);
		endurance = 12;
		healthText.text = health.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= 1){
			timer = 0;
			CmdRegenHealth ();
			CmdRegenMana ();
		}
	}

	[Command]
	void CmdRegenHealth(){
		health += (1 + (int)(0.15f * endurance));
		if (health > maxHealth)
			health = maxHealth;

		controls.UpdateStatText ();
	}

	[Command]
	void CmdRegenMana(){
		mana += (1 + (int)(0.15f * wisdom));
		if (mana > maxMana)
			mana = maxMana;
		
		controls.UpdateStatText ();
	}

	public int GetWpnDamage(){
		return Random.Range (inventory.GetWeapon().GetDamage()[0], inventory.GetWeapon().GetDamage()[1] + 1);
	}


	public void IncreaseExperience(int amount){
		experience += amount;
		while (experience >= experienceToLevel) {
			CmdLevelUp ();
			experience -= experienceToLevel;
			experienceToLevel += 100;
		}

		controls.UpdateStatText ();
	}


	[Command]
	void CmdLevelUp(){
		level++;
		maxHealth += Random.Range (20, 31);
		mana += Random.Range (5, 16);
		strength += Random.Range (1, 3);
		speed += Random.Range (0, 3);
		dexterity += Random.Range (1, 3);
		endurance += Random.Range (0, 2);
		wisdom += Random.Range (0, 3);
		controls.UpdateStatText ();
	}
}
