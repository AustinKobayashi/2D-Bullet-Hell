using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AbstractProjectileMovement : NetworkBehaviour {

    public float Speed;
    public float LifeSpan;
	public string targetTag;
    private float timer;
	private Rigidbody2D rigid;

	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if (timer >= LifeSpan)
            Destroy(this.gameObject);

	}

    public void SetTarget(Vector2 target)
    {
		rigid = GetComponent<Rigidbody2D> ();
		rigid.velocity = target.normalized * Speed;
    }


	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == targetTag && !coll.isTrigger) {
			hit(coll);
			Destroy (this.gameObject);
		}
	}

	public virtual void hit(Collider2D coll)
	{
	}

}
