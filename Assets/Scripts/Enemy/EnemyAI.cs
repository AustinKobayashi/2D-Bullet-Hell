using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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
	

	// Use this for initialization
	void Start () {
		movement = GetComponent<EnemyMovement> ();
		attack = GetComponent<EnemyAttack> ();
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
		NetworkServer.Spawn(drop);
		drop.GetComponent<ItemDrop>().RpcSetItem(i.GetId());
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Player") {
			movement.SetTarget (coll.gameObject);
			attack.SetTarget (coll.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "Player") {
			movement.ClearTarget ();
			attack.ClearTarget ();
		}
	}
}
