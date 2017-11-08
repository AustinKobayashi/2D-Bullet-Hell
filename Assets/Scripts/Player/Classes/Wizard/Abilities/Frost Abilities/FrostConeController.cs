using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostConeController : MonoBehaviour {

    float durationTimer;
    int duration;
    private FrostWizardAbilityControls abilityControls;

    void Update(){
        durationTimer += Time.deltaTime;
        if (durationTimer >= duration)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.tag == "Enemy" && !coll.isTrigger)
            abilityControls.CmdDealDamage(coll.gameObject, 3);
    }

    public void SetDuration(int duration) {
        this.duration = duration;
    }

    public void SetAbilityControls(FrostWizardAbilityControls abilityControls) {
        this.abilityControls = abilityControls;
    }
}
