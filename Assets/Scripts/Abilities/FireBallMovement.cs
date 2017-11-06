using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : AbstractProjectileMovement {

	private AbilityControls abilityControls;

	public FireBallMovement()
	{
		targetTag = "Enemy";
	}

	public override void hit(Collider2D coll) {
		abilityControls.CmdDealDamage(coll.gameObject);
	}
		
	public void SetAbilityControls(AbilityControls abilityControls){
		this.abilityControls = abilityControls;
	}
}
