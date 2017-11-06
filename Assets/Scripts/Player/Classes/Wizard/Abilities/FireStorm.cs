using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStorm : Ability {

	int duration;
    int damage;

	public FireStorm(){
        cooldown = 20f;
		duration = 3;
        damage = 20;
	}

    public int GetDamage(){
        return damage;
    }
}
