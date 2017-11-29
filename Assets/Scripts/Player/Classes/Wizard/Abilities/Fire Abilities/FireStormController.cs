using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireStormController : MonoBehaviour {

    float attackTickTimer = 1;
    float durationTimer;
    int duration = 3;
    List<GameObject> enemies = new List<GameObject>();
    GameObject player;
    int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        attackTickTimer += Time.deltaTime;
        durationTimer += Time.deltaTime;

        if (durationTimer >= duration) {
            NetworkServer.Destroy(gameObject);
            Destroy(gameObject);
        }
        
        if (attackTickTimer >= 1f){
            attackTickTimer = 0;
            try{
                foreach (GameObject enemy in enemies) {
                    if (enemy != null) {
                        try{
                            DealDamage(enemy);
                        } catch (NullReferenceException) {
                            Debug.Log("Enemy died while dealing dmg");
                        }
                    }
                }
            } catch(InvalidOperationException) {
                Debug.Log("Enemy died while enumerating");
            }
        }
	}


    public void SetPlayer(GameObject player) { this.player = player; }

    public void SetDamage(int damage) { this.damage = damage; }



	void OnTriggerEnter2D(Collider2D coll) {
        
		if (coll.tag == "Enemy" && !coll.isTrigger) 
			enemies.Add (coll.gameObject);
	}



	void OnTriggerExit2D(Collider2D coll){
        
		if (enemies.Contains (coll.gameObject))
			enemies.Remove (coll.gameObject);
	}



    public void DealDamage(GameObject enemy) {

        EnemyStatsTest enemyStats = enemy.GetComponent<EnemyStatsTest>();

        // did player kill the enemy
        bool kill = false;

        if (enemyStats != null)
            kill = enemyStats.TakeDamage(damage);
        if (kill && player != null)
            player.GetComponent<AbstractPlayerStats>().IncreaseExperience(enemyStats.GetExperienceGain());
    }
}
