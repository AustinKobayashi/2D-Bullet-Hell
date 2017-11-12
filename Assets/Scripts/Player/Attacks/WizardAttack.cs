using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WizardAttack : AbstractPlayerAttack {

    public GameObject bullet;

	// Use this for initialization
	void Start () {
		stats = GetComponent<PlayerWizardStatsTest> ();
		attackCooldown = 1f / (1.5f + 6.5f * (stats.GetDexterity () / 75f));
	}

	public override void attack(Vector2 target) {
		if (!isLocalPlayer) return;
		CmdAttack(target);
	}

	// Create a bullet and assign the appropriate fields
	[Command]
    void CmdAttack(Vector2 target)
    {
		GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
	    tempBullet.GetComponent<BasicAttackMovement> ().SetPlayerAttack (this);
	    tempBullet.GetComponent<BasicAttackMovement>().SetTarget(target);
	    NetworkServer.Spawn(tempBullet);
    }
}
