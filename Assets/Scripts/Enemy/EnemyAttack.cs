using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyAttack : NetworkBehaviour {

	private GameObject _target;
	public GameObject Bullet;
	public float AttackCooldown;
	private float _timer;
	private EnemyStatsTest _stats;

	// Use this for initialization
	void Start () {
		_stats = GetComponent<EnemyStatsTest> ();
	}
	
	// Update is called once per frame
	void Update () {
		_timer += Time.deltaTime;

		if (_target != null && _timer >= AttackCooldown) {
			Attack ();
			_timer = 0;
		}
	}

	// Attacks in a spray pattern
	void Attack() {
		if (!isServer) return;

		// This is the number of rays / 2 - 1
		int halfNumRays = 2;

		// The angle between each ray
		float deltaTheta = (Mathf.PI / 6f) / halfNumRays;

		Vector2 attackPos = _target.transform.position - transform.position;
		attackPos.Normalize();
		float theta = Mathf.Atan2 (attackPos.y, attackPos.x);

		GameObject tempBullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
		tempBullet.GetComponent<EnemyAttackMovement>().SetTarget(CalculateTarget (theta, 0 , deltaTheta));
		tempBullet.GetComponent<EnemyAttackMovement> ().SetDamage(_stats.GetStrength());
		NetworkServer.Spawn(tempBullet);

		for (int i = 1; i <= halfNumRays; i++) {
			GameObject tempBulletPositive = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
			tempBulletPositive.GetComponent<EnemyAttackMovement>().SetTarget(CalculateTarget (theta, i, deltaTheta));
			tempBulletPositive.GetComponent<EnemyAttackMovement> ().SetDamage(_stats.GetStrength());
			NetworkServer.Spawn(tempBulletPositive);
			GameObject tempBulletNegative = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
			tempBulletNegative.GetComponent<EnemyAttackMovement>().SetTarget(CalculateTarget (theta - 2f * Mathf.PI, -i, deltaTheta));
			tempBulletNegative.GetComponent<EnemyAttackMovement> ().SetDamage(_stats.GetStrength());
			NetworkServer.Spawn(tempBulletNegative);
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
		this._target = target;
	}


	public void ClearTarget(){
		_target = null;
	}
}
