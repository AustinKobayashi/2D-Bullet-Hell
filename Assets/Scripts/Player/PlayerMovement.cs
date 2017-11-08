using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO abstract movement
public class PlayerMovement : MonoBehaviour {

    Vector2 moveVec = Vector2.zero;
    public float speed;
	private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

        moveVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		rigid.velocity = moveVec * speed;
    }
}
