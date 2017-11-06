using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyAttackMovement : AbstractProjectileMovement {

	private EnemyAttack enemyAttack;

	public EnemyAttackMovement()
	{
		targetTag = "Player";
	}

	public override void hit(Collider2D coll) {
		enemyAttack.DealDamage (coll.gameObject);
	}

	public void SetEnemyAttack(EnemyAttack enemyAttack){
		this.enemyAttack = enemyAttack;
	}
}
