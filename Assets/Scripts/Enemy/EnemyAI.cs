using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class EnemyAI : NetworkBehaviour {
/*
 * Its pretty funny but the AI can somehow attack one player and follow another.
 * Sometimes it'll shoot at one player but damage the other as well
 * 		(Bullets going the wrong way but hitting the player anyway)
 * Probably caused by either lack of Network Sync for bullet shooting or target acquisition;
 * TODO: Upgrade Networked AI
 */
	private EnemyMovement movement;
	private EnemyAttack attack;
	public GameObject itemDrop;
	private bool isExiting;
    GameObject target;
    List<GameObject> nearbyPlayers = new List<GameObject>();

	// Use this for initialization
	void Start () {
		movement = GetComponent<EnemyMovement> ();
		attack = GetComponent<EnemyAttack> ();
	}

    void Update(){

        UpdateTarget();
    }


    void UpdateTarget(){
        
        try{
            GameObject closestPlayer = target;

            if (closestPlayer == null){  
                
                nearbyPlayers.Sort(); // To ensure position 0 has a gameobject

                if (nearbyPlayers.Count < 1 || nearbyPlayers[0] == null) // If there are no nearby players return
                    return;

                closestPlayer = nearbyPlayers[0]; // Closest player temporarily is first nearby player
            }

            // Iterate each neaby player and update closes player
            foreach (GameObject player in nearbyPlayers){
                if (Mathf.Abs(Vector2.Distance(transform.position, player.transform.position)) <
                    Mathf.Abs(Vector2.Distance(transform.position, closestPlayer.transform.position)))
                    closestPlayer = player;
            }

            // If the closes player is different than target, update closest player
            if (closestPlayer != target){
                target = closestPlayer;
                movement.SetTarget(target);
                attack.SetTarget(target);
            }
        }

        //for catching nullreference while iterating the list
        // Eg, player leaves ai detection zone while iterating
        catch (System.NullReferenceException){ 
            
            Debug.Log("GOTTA CATCH EM ALL");
        }
    }


	private void OnApplicationQuit() {
		isExiting = true;
	}

	private void OnDestroy() {
		if (isExiting) return;
		if (!isServer) return;
		Drop();
	}



	[Server]
	public void Drop() {
		var drop = Instantiate(itemDrop, transform.position, Quaternion.identity);
		var i = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>().Roll();
		drop.GetComponent<ItemDrop>().SetItem(i.GetId());
		NetworkServer.Spawn(drop);
	}



	void OnTriggerEnter2D(Collider2D coll) {

        if (coll.tag != "Player")
            return;

        if (target == null) {
            target = coll.gameObject;
            movement.SetTarget (target);
            attack.SetTarget (target);
		}

        nearbyPlayers.Add(coll.gameObject);
	}


	void OnTriggerExit2D(Collider2D coll){
        
        if (coll.gameObject == target) {
            target = null;
			movement.ClearTarget ();
			attack.ClearTarget ();
		}

        nearbyPlayers.Remove(coll.gameObject);
	}
}
