using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleMovement : AbstractProjectileMovement {

    private FrostWizardAbilityControls abilityControls;

    public IcicleMovement(){
        TargetTag = "Enemy";
    }
}
