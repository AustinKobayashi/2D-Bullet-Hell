using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public abstract class AbstractStats : NetworkBehaviour {

	[SyncVar] public int health;
	[SyncVar] public int maxHealth;
	[SyncVar] public int strength;
	[SyncVar] public int defence;
	[SyncVar] public int speed;
	[SyncVar] public int dexterity;
	public Text healthText;

   	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Die(){
		Destroy (this.gameObject);
	}

	public int GetHealth(){
		return health;
	}

	public void SetHealth(int health){
		this.health = health;
	}

	[Server]
	public bool TakeDamage(int damage){
		health -= (int)(damage * 0.15f);

		if (defence < (int)(damage * 0.85f)) {
			health -= ((int)(damage * 0.85f) - defence);

		}

		if(healthText)
			healthText.text = health.ToString();

		if (health <= 0) {
			Die ();
			return true;
		}
			
		return false;
	}

	public int GetStrength(){
		return strength;
	}

	public void SetStrength(int strength){
		this.strength = strength;
	}

	public int GetDefence(){
		return defence;
	}

	public void SetDefence(int defence){
		this.defence = defence;
	}

	public int GetSpeed(){
		return speed;
	}

	public int GetDexterity(){
		return dexterity;
	}

	public void SetDexterity(int dexterity){
		this.dexterity = dexterity;
	}
}
