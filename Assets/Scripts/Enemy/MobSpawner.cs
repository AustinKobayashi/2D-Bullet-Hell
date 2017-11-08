using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MobSpawner : NetworkBehaviour {
	public GameObject Enemy;
	public int NumEnemies;
	public List<Vector3> SpawnLocations;
	public bool SpawnOnStart;
	public int SpawnInterval;
	private float _timer;
	
	// Use this for initialization
	void Start () {
		SpawnLocations = new List<Vector3>();
		if (SpawnOnStart) 
			AddRandomSpawns(NumEnemies);
			SpawnEnemies();
	}

	public void AddRandomSpawns(int n) {
		for (int i = 0; i < n; i++) {
			SpawnLocations.Add(new Vector3(
				Random.Range(-8f, 8f), 
				Random.Range(-8f, 8f), 
				0));
		}
	}

	public void SpawnEnemies() {
		for (int i = 0; i < NumEnemies; i++) {
			var enemy = Instantiate(Enemy, RandomSpawn(), Quaternion.identity);
			NetworkServer.Spawn(enemy);
		}
	}

	private Vector3 RandomSpawn() {
		return SpawnLocations[Random.Range(0, SpawnLocations.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		if (SpawnInterval <= 0) return;
		_timer += Time.deltaTime;
		if (!(_timer > SpawnInterval)) return;
		SpawnEnemies();
		_timer = 0;
	}
}
