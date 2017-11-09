using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AbstractPlayerAttack : NetworkBehaviour {

    public float attackCooldown;
    public float timer;
	public AbstractPlayerStats stats;
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

		if (Input.GetMouseButton(0) && timer >= attackCooldown) {
			attack(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
			timer = 0;
		}
	}

	// Create a bullet and assign the appropriate fields
	public virtual void attack(Vector2 target) {
		
	}

	// Called from the bullet if it hits an enemy
	[Command]
	public void CmdDealDamage(GameObject enemy){

		EnemyStatsTest enemyStats = enemy.GetComponent<EnemyStatsTest> ();

		// did player kill the enemy
		bool kill = false;

		if (enemyStats != null) 
			kill = enemyStats.TakeDamage ((int)(stats.GetWpnDamage() * (0.5f + stats.GetStrength() / 50f)));

		if (kill)
			stats.IncreaseExperience (enemyStats.GetExperienceGain ());
	}
}
