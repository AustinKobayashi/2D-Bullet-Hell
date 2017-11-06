using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStormController : MonoBehaviour {

    float attackTickTimer = 1;
    float durationTimer;
    int duration = 3;
    private FireWizardAbilityControls abilityControls;
    List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        attackTickTimer += Time.deltaTime;
        durationTimer += Time.deltaTime;

        if (durationTimer >= duration)
            Destroy(this.gameObject);
        
        if (attackTickTimer >= 1f){
            attackTickTimer = 0;
            try{
                foreach (GameObject enemy in enemies) {
                    if (enemy != null)
                        abilityControls.CmdDealDamage(enemy, 3);
                }
            }catch(InvalidOperationException){
                Debug.Log("Enemy died while enumerating");
            }
        }
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Enemy" && !coll.isTrigger) {
			enemies.Add (coll.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (enemies.Contains (coll.gameObject))
			enemies.Remove (coll.gameObject);
	}

    public void SetAbilityControls(FireWizardAbilityControls abilityControls) {
        this.abilityControls = abilityControls;
    }
}
