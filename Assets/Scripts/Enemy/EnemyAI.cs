using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private EnemyMovement movement;
	private EnemyAttack attack;
	public GameObject itemDrop;
	

	// Use this for initialization
	void Start () {
		movement = GetComponent<EnemyMovement> ();
		attack = GetComponent<EnemyAttack> ();
	}

	private void OnDestroy() {
		Instantiate(itemDrop, transform.position, Quaternion.identity);
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
