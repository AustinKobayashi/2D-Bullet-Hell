using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

	public float fireBallSpeed;
	public float fireBallLife;
	private float timer;
	private Rigidbody2D rigid;
	private AbilityControls abilityControls;


	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
        if (timer >= fireBallLife)
            Destroy(this.gameObject);
	}

	public void SetTarget(Vector2 target)
	{
		rigid = GetComponent<Rigidbody2D> ();
		rigid.velocity = target.normalized * fireBallSpeed;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Enemy" && !coll.isTrigger) {
			abilityControls.CmdDealDamage (coll.gameObject, 1);
			Destroy (this.gameObject);
		}
	}
		
	public void SetAbilityControls(AbilityControls abilityControls){
		this.abilityControls = abilityControls;
	}
}
