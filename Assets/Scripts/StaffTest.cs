using UnityEngine;
using System.Collections;

public class StaffTest : Weapon {

	public StaffTest(){
		SetId (0);
		damage = new int[] {10, 25};
		projectileSpeed = 8;
		range = 1;
		SetImage ("staff");
		SetItemType (ItemTypes.weapon);
	}
}
