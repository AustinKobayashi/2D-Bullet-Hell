using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyMovement : EnemyMovement {

    public NavMeshComponent curNavMeshComponent;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update(){
        if (target == null && Mathf.Abs((moveTarget - (Vector2)transform.position).magnitude) < buffer)
            SetPatrolPoint();

        if(target){
            MoveTowardsTarget();
        }
    }



    void MoveTowardsTarget(){
        if (curNavMeshComponent.IsInsideNavMeshComponent(target.transform.position))
            Debug.Log("player is in same navmesh component");
    }



    void SetPatrolPoint(){
        moveTarget = curNavMeshComponent.RandomPointInsideNavMeshComponent();
    }



    public void UpdateNavMeshComponent(NavMeshComponent curNavMeshComponent) { this.curNavMeshComponent = curNavMeshComponent; }
}
