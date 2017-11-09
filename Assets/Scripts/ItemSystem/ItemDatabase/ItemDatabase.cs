using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ItemDatabase : NetworkBehaviour {
	
	/*
	 * Since pretty much all players and some objects need access to the ItemDatabase, it should not be serverside.
	 * Changes to the database should be made by the server, then pushed with ClientRpc to all clients.
	 * TODO: Network proof the database while keeping it up to date on all clients
	 */
	List<Item> database;
	private ItemRarityDatabase IRDB;

	void Start() {
		database = new List<Item> {new StaffTest(), new ArmourTest(), new SwordTest(), new PotionTest(), new ScrollTest()};
		IRDB = gameObject.GetComponent<ItemRarityDatabase>();
		IRDB.updateDB(database);
	}

	public Item GetItem(int index){
		if (index == -1)
			return null;
		
		return database [index];
	}

	private ItemRarity rand() {
		float rand = Random.Range(0f, 100f);
		if (rand < 1) return ItemRarity.Mythic;
		if (rand < 10) return ItemRarity.Legendary;
		if (rand < 30) return ItemRarity.Epic;
		if (rand < 60) return ItemRarity.Rare;
		return ItemRarity.Common;
	}
	
	public Item Roll() {
		int i = -1;
		while (i < 0) {
			i = IRDB.getRandomOfRarity(rand());
		}
		return database[i];
	}
}
