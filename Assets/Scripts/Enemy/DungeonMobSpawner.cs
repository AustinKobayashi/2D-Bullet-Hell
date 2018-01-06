using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DungeonMobSpawner : NetworkBehaviour {

    public GameObject enemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEnemies(List<Room> rooms){
        var enemy = Instantiate(enemies, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(enemy);
        enemy.GetComponent<DungeonEnemyMovement>().UpdateNavMeshComponent(rooms[0].GetNavMeshComponent());
    }
}
