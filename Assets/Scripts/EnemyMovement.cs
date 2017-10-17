using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	private GameObject target;
	private Vector2 startPos;
	private Vector2 moveTarget;
	private bool atMoveTarget;
	public int patrolDistance;
	public float buffer;
	public int moveSpeed;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
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

		Move ();
	}

		

	void Move(){
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
