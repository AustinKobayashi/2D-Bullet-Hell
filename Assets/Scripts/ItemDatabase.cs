using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ItemDatabase : NetworkBehaviour {

	Item[] database;

	void Start(){
		database = new Item[10];
		database [0] = new StaffTest ();
		database [1] = new ArmourTest ();
		database [2] = new SwordTest ();
	}

	[Server]
	public Item GetItem(int index){
		if (index == -1)
			return null;
		
		return database [index];
	}
}
