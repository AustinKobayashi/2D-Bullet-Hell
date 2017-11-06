using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour {

    public GameObject bullet;
    public float attackCooldown;
    private float timer;
	private PlayerWizardStatsTest stats;

	// Use this for initialization
	void Start () {
		stats = GetComponent<PlayerWizardStatsTest> ();
		attackCooldown = 1f / (1.5f + 6.5f * (stats.GetDexterity () / 75f));
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= attackCooldown) {
			CmdAttack(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            timer = 0;
        }
	}

	// Create a bullet and assign the appropriate fields
	[Command]
    void CmdAttack(Vector2 target)
    {
		GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		tempBullet.GetComponent<AttackController>().SetTarget(target);
		tempBullet.GetComponent<AttackController> ().SetPlayerAttack (this);
    }
		

	// Called from the bullet if it hits an enemy
	[Command]
	public void CmdDealDamage(GameObject enemy){

		if (!isLocalPlayer)
			return;

		EnemyStatsTest enemyStats = enemy.GetComponent<EnemyStatsTest> ();

		// did player kill the enemy
		bool kill = false;

		if (enemyStats != null) 
			kill = enemyStats.TakeDamage ((int)(stats.GetWpnDamage() * (0.5f + stats.GetStrength() / 50f)));

		if (kill)
			stats.IncreaseExperience (enemyStats.GetExperienceGain ());
	}
}
