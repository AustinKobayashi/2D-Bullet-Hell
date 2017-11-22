using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !!!TODO Must also drop aggro
public class IceBlock : Ability {

    int duration;

    public IceBlock()
    {
        //cooldown = 15f;
        cooldown = 10f;
        duration = 5;
    }

    public int GetDuration(){
        return duration;
    }
}
