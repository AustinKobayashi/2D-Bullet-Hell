using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO need to make stun
public class FrostCone : Ability {

    int duration;
    int damage;

    public FrostCone() {
        cooldown = 15f;
        duration = 1;
        damage = 15;
    }

    public int GetDamage() {
        return damage;
    }

    public int GetDuration(){
        return duration;
    }
}
