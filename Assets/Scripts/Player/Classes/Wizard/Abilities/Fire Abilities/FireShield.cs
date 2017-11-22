using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : Ability {

	int duration;

	public FireShield(){
		cooldown = 15f;
		duration = 5;
	}

    public int GetDuration(){
        return duration;
    }
}
