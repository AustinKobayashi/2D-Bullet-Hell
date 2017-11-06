using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleMovement : AbstractProjectileMovement {

    private FrostWizardAbilityControls abilityControls;

    public IcicleMovement(){
        targetTag = "Enemy";
    }

    public override void hit(Collider2D coll){
        abilityControls.CmdDealDamage(coll.gameObject, 1);
    }

    public void SetAbilityControls(FrostWizardAbilityControls abilityControls){
        this.abilityControls = abilityControls;
    }
}
