using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionTest : Consumable {

	public PotionTest(){
		SetId (3);
		SetImage ("potion");
		SetItemType (ItemTypes.consumable);
		setItemRarity(ItemRarity.Epic);
	}
}
