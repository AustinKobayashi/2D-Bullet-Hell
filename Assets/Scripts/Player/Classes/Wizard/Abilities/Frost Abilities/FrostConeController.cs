using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FrostConeController : MonoBehaviour {

    float durationTimer;
    int duration;
    private FrostWizardAbilityControls abilityControls;

    void Awake(){
        StartCoroutine(CastDuration());
    }

    IEnumerator CastDuration(){

        yield return new WaitForSeconds(new FrostCone().GetDuration());
        NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.tag == "Enemy" && !coll.isTrigger){

            try{
                abilityControls.CmdDealDamage(coll.gameObject, 3);
            } catch (System.NullReferenceException){
                
            }
        }
    }

    public void SetDuration(int duration) {
        this.duration = duration;
    }

    public void SetAbilityControls(FrostWizardAbilityControls abilityControls) {
        this.abilityControls = abilityControls;
    }
}
