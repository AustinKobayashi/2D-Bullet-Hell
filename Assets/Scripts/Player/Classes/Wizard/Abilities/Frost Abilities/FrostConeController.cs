using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FrostConeController : MonoBehaviour {

    float durationTimer;
    GameObject player;
    int damage;

    void Awake(){
        StartCoroutine(CastDuration());
    }


    IEnumerator CastDuration(){

        yield return new WaitForSeconds(new FrostCone().GetDuration());
        NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
        yield return null;
    }



    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.tag == "Enemy" && !coll.isTrigger){
            Debug.Log("called");
            try{
                DealDamage(coll.gameObject);
            } catch (System.NullReferenceException){
                
            }
        }
    }



    public void SetPlayer(GameObject player) { this.player = player; }

    public void SetDamage(int damage) { this.damage = damage; }



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
