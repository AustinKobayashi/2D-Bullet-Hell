using UnityEngine;

public class ItemDrop : MonoBehaviour {
	private Item _item;
	private ItemDatabase _database;
	// Use this for initialization
	void Start () {
		_database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
		rollItem();
	}

	void rollItem() {
		_item = _database.Roll();
		gameObject.GetComponent<SpriteRenderer>().sprite = _item.GetItemImage();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.gameObject.CompareTag("Player")) return;
		other.gameObject.GetComponent<Inventory>().CmdAddItem(_item);
		Destroy(gameObject);
	}
}
