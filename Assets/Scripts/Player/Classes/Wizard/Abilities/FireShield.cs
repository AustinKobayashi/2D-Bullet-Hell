using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : Ability {

	int duration;

	public FireShield(){
		cooldown = 1f;
		duration = 5;
	}
}
