using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public GameObject target;
    protected Vector2 startPos;
    protected Vector2 moveTarget;
    protected bool atMoveTarget;
    public int patrolDistance;
    public float buffer;
    public int moveSpeed;
    protected Rigidbody2D rigid;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        rigid = GetComponent<Rigidbody2D> ();
    }
    
    // Update is called once per frame
    void Update () {

        if(Mathf.Abs((moveTarget - (Vector2)transform.position).magnitude) < buffer)
            SetPatrolPoint();
    }



    void FixedUpdate(){
        Move ();
    }



    void Move(){
        if(Vector2.Distance(transform.position, moveTarget) > 1)
            transform.position = Vector2.MoveTowards (transform.position, moveTarget, moveSpeed * Time.deltaTime);
    }


    // TODO make sure that moveTarget isnt inside a collider    
    void SetPatrolPoint(){
        moveTarget = (Random.insideUnitCircle.normalized * patrolDistance) + startPos;
    }



    public void SetTarget(GameObject target){
        this.target = target;
    }



    public void ClearTarget(){
        target = null;
        startPos = transform.position;
    }
}
