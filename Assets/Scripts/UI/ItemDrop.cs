using UnityEngine;
using UnityEngine.Networking;

public class ItemDrop : NetworkBehaviour {
	[SyncVar]
	private int _itemId;
	private Item _item;
	private ItemDatabase _database;
	private bool _updated;
	// Use this for initialization

	public void SetItem(int i) {
		_itemId = i;
		UpdateItem();
	}

	private void UpdateItem() {
		_database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
		_item = _database.GetItem(_itemId);
		gameObject.GetComponent<SpriteRenderer>().sprite = _item.GetItemImage();
		_updated = true;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!isServer || !other.gameObject.CompareTag("Player")) return;
		other.gameObject.GetComponent<Inventory>().CmdAddItem(_item);
		Destroy(gameObject);
	}

	void Update() {
		if (!_updated) {
			UpdateItem();
		}
	}
	
}
