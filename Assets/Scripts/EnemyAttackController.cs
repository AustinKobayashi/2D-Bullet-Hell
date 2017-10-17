using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyAttackController : MonoBehaviour {

	public float bulletSpeed;
	public float attackLife;
	private float timer;
	private Rigidbody2D rigid;
	private EnemyAttack enemyAttack;


	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (timer >= attackLife)
			Destroy(this.gameObject);

	}

	public void SetTarget(Vector2 target)
	{
		rigid = GetComponent<Rigidbody2D> ();
		rigid.velocity = target.normalized * bulletSpeed;
	}


	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Player" && !coll.isTrigger) {
			enemyAttack.DealDamage (coll.gameObject);
			Destroy (this.gameObject);
		}
	}


	public void SetEnemyAttack(EnemyAttack enemyAttack){
		this.enemyAttack = enemyAttack;
	}
}
