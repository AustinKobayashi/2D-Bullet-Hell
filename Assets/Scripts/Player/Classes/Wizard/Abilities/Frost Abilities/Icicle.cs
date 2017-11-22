using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : Ability{

    int damage;

    public Icicle ()
    {
        //cooldown = 5f;
        cooldown = 10f;
        damage = 10;
    }

    public int GetDamage()
    {
        return damage;
    }
}
