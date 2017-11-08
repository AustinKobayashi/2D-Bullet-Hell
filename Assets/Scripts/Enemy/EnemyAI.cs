using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyAI : NetworkBehaviour {

	private EnemyMovement movement;
	private EnemyAttack attack;
	public GameObject itemDrop;
	

	// Use this for initialization
	void Start () {
		movement = GetComponent<EnemyMovement> ();
		attack = GetComponent<EnemyAttack> ();
	}

	private void OnDestroy() {
		RpcDropItem();
	}

	private void RpcDropItem() {
		var drop = Instantiate(itemDrop, transform.position, Quaternion.identity);
		NetworkServer.Spawn(drop);
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
