using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : AbstractProjectileMovement {

    private FireWizardAbilityControls abilityControls;

	public FireBallMovement() {
		TargetTag = "Enemy";
	}
}
