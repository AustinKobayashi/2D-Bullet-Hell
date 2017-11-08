using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : AbstractProjectileMovement {

    private FireWizardAbilityControls abilityControls;

	public FireBallMovement() {
		targetTag = "Enemy";
	}

	public override void hit(Collider2D coll) {
		abilityControls.CmdDealDamage(coll.gameObject, 1);
	}
		
    public void SetAbilityControls(FireWizardAbilityControls abilityControls){
		this.abilityControls = abilityControls;
	}
}
