using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTest : AbilityItem {

	public ScrollTest(){
		SetId (4);
		SetImage ("scroll");
		SetItemType (ItemTypes.ability);
		damage = new int[] {50, 75};
		projectileSpeed = 8;
		range = 1;
	}
}
