using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyAI : AbstractEnemyAI {

    public float collRadius;

    void Awake(){
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();
        collRadius = GetComponent<CircleCollider2D>().radius;
    }


    void OnTriggerStay2D(Collider2D coll) {

        if (coll.tag != "Player")
            return;

        if (target == null) {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, coll.transform.position - transform.position, collRadius, LayerMask.GetMask("Player"));

            if (hit && hit.transform.gameObject.tag == "Player") {
                target = coll.gameObject;
                movement.SetTarget(target);
                attack.SetTarget(target);
            } else {
                target = null;
                movement.ClearTarget();
                attack.ClearTarget();
            }
        }
    }


    void OnTriggerExit2D(Collider2D coll){

        if (coll.gameObject == target) {
            target = null;
            movement.ClearTarget();
            attack.ClearTarget();
        }
    }
}
