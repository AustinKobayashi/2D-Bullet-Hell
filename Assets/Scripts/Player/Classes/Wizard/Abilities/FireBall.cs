using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBall : Ability{

	float cooldown;
    int damage;

	public FireBall(){
		cooldown = 2f;
        damage = 10;
	}

    public int GetDamage() {
        return damage;
    }
}
