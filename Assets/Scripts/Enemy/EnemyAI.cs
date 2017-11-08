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
		CmdDropItem();
	}
	[Command]
	private void CmdDropItem() {
		var drop = Instantiate(itemDrop, transform.position, Quaternion.identity);
		NetworkServer.Spawn(drop);
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
