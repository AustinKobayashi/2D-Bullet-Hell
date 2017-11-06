using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : Ability {

    int duration;

    public IceBlock()
    {
        cooldown = 15f;
        duration = 5;
    }
}
