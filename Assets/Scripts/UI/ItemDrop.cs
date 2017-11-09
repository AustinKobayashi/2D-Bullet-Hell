using UnityEngine;
using UnityEngine.Networking;

public class ItemDrop : NetworkBehaviour {
	private Item _item;

	private ItemDatabase _database;
	// Use this for initialization

	[ClientRpc]
	public void RpcSetItem(int i) {
		Debug.Log("Setting item to : " + i)	;
		_database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
		_item = _database.GetItem(i);
		Debug.Log("_item immage: " + _item.GetItemImage());
		Debug.Log("GO Image: " + gameObject.GetComponent<SpriteRenderer>().sprite);
		gameObject.GetComponent<SpriteRenderer>().sprite = _item.GetItemImage();
		Debug.Log("GO Image: " + gameObject.GetComponent<SpriteRenderer>().sprite);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.gameObject.CompareTag("Player")) return;
		other.gameObject.GetComponent<Inventory>().CmdAddItem(_item);
		Destroy(gameObject);
	}
}
