using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class WorldEnemyAI : AbstractEnemyAI {

	void OnTriggerEnter2D(Collider2D coll) {

        if (coll.tag != "Player")
            return;

        if (target == null) {
            target = coll.gameObject;
            attack.SetTarget (target);
		}
	}


	void OnTriggerExit2D(Collider2D coll){
        
        if (coll.gameObject == target) {
            target = null;
			attack.ClearTarget ();
		}
	}
}
