using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyAI : NetworkBehaviour {
/*
 * Its pretty funny but the AI can somehow attack one player and follow another.
 * TODO: Upgrade AI (not important)
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

	// Update is called once per frame
	void Update () {
	
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
