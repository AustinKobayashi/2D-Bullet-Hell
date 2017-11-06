using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityItem : Item {

	protected int[] damage;
	protected float projectileSpeed;
	protected float range;

	public int[] GetDamage(){
		return damage;
	}
}
