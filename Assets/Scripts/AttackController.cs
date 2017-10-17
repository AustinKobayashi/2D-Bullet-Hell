using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttackController : MonoBehaviour {

    public float bulletSpeed;
    public float attackLife;
    private float timer;
	private Rigidbody2D rigid;
	private PlayerAttack playerAttack;


	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer >= attackLife)
            Destroy(this.gameObject);

	}

    public void SetTarget(Vector2 target)
    {
		rigid = GetComponent<Rigidbody2D> ();
		rigid.velocity = target.normalized * bulletSpeed;
    }


	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Enemy" && !coll.isTrigger) {
			playerAttack.CmdDealDamage (coll.gameObject);
			Destroy (this.gameObject);
		}
	}


	public void SetPlayerAttack(PlayerAttack playerAttack){
		this.playerAttack = playerAttack;
	}
}
