using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

    Vector2 moveVec = Vector2.zero;
    public float speed;
	private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) return;
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) return;
		if (GetComponent<InventoryHandler>().MenuOpen) {
			rigid.velocity = Vector2.zero;
			return;
		}

        moveVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		rigid.velocity = moveVec * speed;
    }
}
