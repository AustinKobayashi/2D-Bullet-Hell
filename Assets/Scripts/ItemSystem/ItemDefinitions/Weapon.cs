using UnityEngine;
using System.Collections;

public class Weapon : Item {

	protected int[] damage;
	protected float projectileSpeed;
	protected float range;

	public int[] GetDamage(){
		return damage;
	}
}
