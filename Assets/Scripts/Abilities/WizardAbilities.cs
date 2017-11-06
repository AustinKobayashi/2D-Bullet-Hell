﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WizardAbilities : Abilities {

	public GameObject fireBallPrefab;
	public GameObject fireShieldPrefab;

	// Use this for initialization
	void Awake () {
		firstAbility = new FireBall ();
		secondAbility = new FireShield ();
	}

	// Update is called once per frame
	void Update () {

	}

	[Server]
	public void CastFirstAbility(Vector2 target, AbilityControls abilityControls){
		GameObject tempFireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity) as GameObject;
		tempFireBall.GetComponent<FireBallController>().SetTarget(target);
		tempFireBall.GetComponent<FireBallController> ().SetAbilityControls (abilityControls);
	}

	[Server]
	public void CastSecondAbility(PlayerWizardStatsTest stats){
		GameObject tempFireShield = Instantiate (fireShieldPrefab, this.transform) as GameObject;
		tempFireShield.GetComponent<FireShieldController> ().SetStats (stats);
		tempFireShield.GetComponent<FireShieldController>().AddDefence((int)(stats.GetDefence() * 0.25f), 5);
	}
}