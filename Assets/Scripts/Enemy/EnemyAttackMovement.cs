using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyAttackMovement : AbstractProjectileMovement {


	public EnemyAttackMovement()
	{
		TargetTag = "Player";
	}

	public override void hit(Collider2D coll) {
		if (!isServer) return;
		AbstractPlayerStats playerStats = coll.gameObject.GetComponent<AbstractPlayerStats> ();

		if (playerStats != null) {
			playerStats.TakeDamage(Damage);
		}
	}
}
