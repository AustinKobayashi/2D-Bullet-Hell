using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public GameObject target;
	private Vector2 startPos;
	private Vector2 moveTarget;
	private bool atMoveTarget;
	public int patrolDistance;
	public float buffer;
	public int moveSpeed;
	private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (target != null) {
			if (Mathf.Abs ((target.transform.position - transform.position).magnitude) > buffer)
				moveTarget = target.transform.position;
			else
				moveTarget = transform.position;
		}else if(target == null && Mathf.Abs((moveTarget - (Vector2)transform.position).magnitude) < buffer)
			SetPatrolPoint ();
	}

	void FixedUpdate(){
		Move ();
	}


	void Move(){
		if(Vector2.Distance(transform.position, moveTarget) > 1)
			transform.position = Vector2.MoveTowards (transform.position, moveTarget, moveSpeed * Time.deltaTime);
	}

		
	void SetPatrolPoint(){
		moveTarget = (Random.insideUnitCircle * patrolDistance) + startPos;
	}



	public void SetTarget(GameObject target){
		this.target = target;
	}



	public void ClearTarget(){
		target = null;
		startPos = transform.position;
	}
}
