using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item {

	int id;
	Sprite itemImage;

	public void SetId(int id){
		this.id = id;
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
