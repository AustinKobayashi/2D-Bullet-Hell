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
		
	[Server]
	public void DealDamage(GameObject player){

		AbstractPlayerStats playerStats = player.GetComponent<AbstractPlayerStats> ();

		if (playerStats != null) 
			playerStats.TakeDamage (stats.GetStrength ());
	}

	// Attacks in a spray pattern
	void Attack(){

		// This is the number of rays / 2 - 1
		int halfNumRays = 2;

		// The angle between each ray
		float deltaTheta = (Mathf.PI / 6f) / halfNumRays;

		Vector2 attackPos = target.transform.position - transform.position;
		attackPos.Normalize();
		float theta = Mathf.Atan2 (attackPos.y, attackPos.x);

		GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		tempBullet.GetComponent<EnemyAttackMovement>().SetTarget(CalculateTarget (theta, 0 , deltaTheta));
		tempBullet.GetComponent<EnemyAttackMovement> ().SetEnemyAttack (this);

		for (int i = 1; i <= halfNumRays; i++) {
			GameObject tempBulletPositive = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
			tempBulletPositive.GetComponent<EnemyAttackMovement>().SetTarget(CalculateTarget (theta, i, deltaTheta));
			tempBulletPositive.GetComponent<EnemyAttackMovement> ().SetEnemyAttack (this);

			GameObject tempBulletNegative = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
			tempBulletNegative.GetComponent<EnemyAttackMovement>().SetTarget(CalculateTarget (theta - 2f * Mathf.PI, -i, deltaTheta));
			tempBulletNegative.GetComponent<EnemyAttackMovement> ().SetEnemyAttack (this);
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


	public void ClearTarget(){
		target = null;
	}
}
