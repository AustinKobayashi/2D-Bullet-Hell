using UnityEngine;
using System.Collections;

public class SwordTest : Weapon {

	public SwordTest(){
		SetId (2);
		damage = new int[] {10, 25};
		projectileSpeed = 8;
		range = 1;
		SetImage ("sword");
		SetItemType (ItemTypes.weapon);
	}
}
