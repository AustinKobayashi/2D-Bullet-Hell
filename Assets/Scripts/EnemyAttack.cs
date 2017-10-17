using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyAttack : NetworkBehaviour {

	private GameObject target;
	public GameObject bullet;
	public float attackCooldown;
	private float timer;
	private EnemyStatsTest stats;

	// Use this for initialization
	void Start () {
		stats = GetComponent<EnemyStatsTest> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (target != null && timer >= attackCooldown) {
			Attack ();
			timer = 0;
		}
	}

	void Attack(){

		// This is the number of rays / 2 - 1
		int halfNumRays = 2;

		// The angle between each ray
		float deltaTheta = (Mathf.PI / 6f) / halfNumRays;

		Vector2 attackPos = target.transform.position - transform.position;
		attackPos.Normalize();
		float theta = Mathf.Atan2 (attackPos.y, attackPos.x);
		float xPos, yPos;

		GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		tempBullet.GetComponent<EnemyAttackController>().SetTarget(CalculateTarget (theta, 0 , deltaTheta));
		tempBullet.GetComponent<EnemyAttackController> ().SetEnemyAttack (this);

		for (int i = 1; i <= halfNumRays; i++) {
			GameObject tempBulletPositive = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
			tempBulletPositive.GetComponent<EnemyAttackController>().SetTarget(CalculateTarget (theta, i, deltaTheta));
			tempBulletPositive.GetComponent<EnemyAttackController> ().SetEnemyAttack (this);

			GameObject tempBulletNegative = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
			tempBulletNegative.GetComponent<EnemyAttackController>().SetTarget(CalculateTarget (theta - 2f * Mathf.PI, -i, deltaTheta));
			tempBulletNegative.GetComponent<EnemyAttackController> ().SetEnemyAttack (this);


		}
	}


	// Calculatest the position of theta + i * deltaTheta
	Vector2 CalculateTarget (float theta, int i, float deltaTheta){

		float xPos, yPos;
		xPos = Mathf.Cos (theta + i * deltaTheta);
		yPos = Mathf.Sin (theta + i * deltaTheta);
		Vector2 target = new Vector2(xPos, yPos);
		return target;
	}

	public void SetTarget(GameObject target){
		this.target = target;
	}


	[Server]
	public void DealDamage(GameObject player){

		AbstractPlayerStats playerStats = player.GetComponent<AbstractPlayerStats> ();
		// did player kill the enemy
		bool kill = false;

		if (playerStats != null) 
			kill = playerStats.TakeDamage (stats.GetStrength ());
		
	}
}
