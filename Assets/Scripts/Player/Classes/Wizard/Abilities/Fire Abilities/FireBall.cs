using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBall : Ability{

    int damage;

	public FireBall(){
		cooldown = 5f;
        damage = 10;
	}

    public int GetDamage() {
        return damage;
    }
}
