using UnityEngine;
using System.Collections;

public class ArmourTest : Armour {

	public ArmourTest(){
		SetId (1);
		defence = 10;
		SetImage ("armour");
		SetItemType (ItemTypes.armour);
	}
}
