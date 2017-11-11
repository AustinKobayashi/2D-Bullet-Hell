using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StatsHandler : NetworkBehaviour {
	private AbstractPlayerStats _stats;
	private Text _playerName,
		_health,
		_strength,
		_defense,
		_speed,
		_dexterity;
	// Use this for initialization
	void Start () {
		var menu = transform.Find("Menu").Find("Stats");
		_playerName = menu.Find("PlayerName").GetComponent<Text>();
		_health = menu.Find("Health").GetComponent<Text>();
		_strength = menu.Find("Strength").GetComponent<Text>();
		_defense = menu.Find("Defense").GetComponent<Text>();
		_speed = menu.Find("Speed").GetComponent<Text>();
		_dexterity = menu.Find("Dexterity").GetComponent<Text>();
		_stats = GetComponent<AbstractPlayerStats>();
	}

	public void UpdateText() {
		_playerName.text = "Player";
		_health.text = "Health: " + _stats.GetHealth() + "/" + _stats.getMaxHealth();
		_strength.text = "Strength: " + _stats.GetStrength();
		_defense.text = "Defense: " + _stats.GetDefence();
		_speed.text = "Speed: " + _stats.GetSpeed();
		_dexterity.text = "Dexterity: " + _stats.GetDexterity();
	}
	
}
