using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
	private AbstractPlayerStats stats;
	public GameObject Player;
	public GameObject Bar;
	public Image Health;
	// Use this for initialization
	void Start ()
	{
		stats = Player.GetComponent<AbstractPlayerStats>();
		Health = Bar.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Health.fillAmount = stats.GetHealth() / 100f;
		Debug.Log(stats.GetHealth() / 100f);
	}

}
