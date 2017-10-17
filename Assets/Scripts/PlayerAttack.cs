using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttack : NetworkBehaviour {

    public GameObject bullet;
    public float attackCooldown;
    private float timer;
    PlayerMovement playerMovement;
	private PlayerWizardStatsTest stats;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
		stats = GetComponent<PlayerWizardStatsTest> ();
		attackCooldown = 1f / (1.5f + 6.5f * (stats.GetDexterity () / 75f));
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= attackCooldown)
        {
            Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            timer = 0;
        }
	}


    void Attack(Vector2 target)
    {
		GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		tempBullet.GetComponent<AttackController>().SetTarget(target);
		tempBullet.GetComponent<AttackController> ().SetPlayerAttack (this);
    }

	//Client tells server to do something = command
	//Must begin with cmd
	//[Command] means that this is run on the server
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
