using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {
	private AbstractStats stats;
	public GameObject Mob;
	public GameObject Bar;
	private Image _health;
	// Use this for initialization
	void Start () {
		stats = Mob.GetComponent<AbstractStats>();
		_health = Bar.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		var fillamt = stats.GetHealth() / (float) stats.getMaxHealth();
		_health.fillAmount = fillamt < 0 ? 0 : fillamt;
	}

}
