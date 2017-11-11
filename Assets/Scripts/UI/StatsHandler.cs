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
		_dexterity,
		_mana,
		_endurance,
		_wisdom,
		_level,
		_experience;
	// Use this for initialization
	void Start () {
		var menu = transform.Find("Menu").Find("Stats");
		_playerName = menu.Find("PlayerName").GetComponent<Text>();
		_health = menu.Find("Health").GetComponent<Text>();
		_strength = menu.Find("Strength").GetComponent<Text>();
		_defense = menu.Find("Defense").GetComponent<Text>();
		_speed = menu.Find("Speed").GetComponent<Text>();
		_dexterity = menu.Find("Dexterity").GetComponent<Text>();
		_mana = menu.Find("Mana").GetComponent<Text>();
		_endurance = menu.Find("Endurance").GetComponent<Text>();
		_wisdom = menu.Find("Wisdom").GetComponent<Text>();
		_level = menu.Find("Level").GetComponent<Text>();
		_experience = menu.Find("Experience").GetComponent<Text>();
		_stats = GetComponent<AbstractPlayerStats>();
	}

	public void UpdateText() {
		_playerName.text = _stats.GetPlayerName();
		_health.text = "Health: " + _stats.GetHealth() + "/" + _stats.getMaxHealth();
		_strength.text = "Strength: " + _stats.GetStrength();
		_defense.text = "Defense: " + _stats.GetDefence();
		_speed.text = "Speed: " + _stats.GetSpeed();
		_dexterity.text = "Dexterity: " + _stats.GetDexterity();
		_mana.text = "Mana: " + _stats.GetMana() + "/" + _stats.GetMaxMana();
		_endurance.text = "Endurance: " + _stats.GetEndurance();
		_wisdom.text = "Wisdom: " + _stats.GetWisdom();
		_level.text = "Level: " + _stats.GetLevel();
		_experience.text = "Experience: " + _stats.GetExperience() + "/" + _stats.GetMaxExperience();
	}
	
}
