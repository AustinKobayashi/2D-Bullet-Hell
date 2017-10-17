using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item {

	int id;
	Sprite itemImage;
	ItemTypes itemType;

	public void SetId(int id){
		this.id = id;
	}

	public void SetItemType(ItemTypes itemType){
		this.itemType = itemType;
	}

	public ItemTypes GetItemType(){
		return itemType;
	}

	public int GetId(){
		return id;
	}

	public void SetImage(string path){
		itemImage = Resources.Load<Sprite> ("ItemImages/" + path);
	}

	public Sprite GetItemImage(){
		return itemImage;
	}
}
