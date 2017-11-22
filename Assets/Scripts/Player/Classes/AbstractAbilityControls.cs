using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * TODO: implement abilities locked depending on level
 * TODO: Fix networked behavior for abilities on client
 * Right now abilities can only be cast by the host
 * and the ability is shot from the host + all clients, although only the host can see it.
 * TODO: Add ability gameObjects to network spawner OR add them as children to player
 * TODO: Check for Local Player Authority when casting spells
 * TODO: Properly instantiate abilities with NetworkServer.spawn() so everyone can see them.
 */
public class AbstractAbilityControls : NetworkBehaviour {

    protected float cooldown1;
    float cooldownTimer1;
    protected bool onCoolDown1;

    protected float cooldown2;
    float cooldownTimer2;
    protected bool onCoolDown2;

    protected float cooldown3;
    float cooldownTimer3;
    protected bool onCoolDown3;

    protected PlayerWizardStatsTest stats;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {


    }


    [Command]
    protected void CmdCalculateCooldown(){
        
        if (onCoolDown1)
            cooldownTimer1 += Time.deltaTime;

        if (onCoolDown2)
            cooldownTimer2 += Time.deltaTime;

        if (onCoolDown3)
            cooldownTimer3 += Time.deltaTime;

        if (cooldownTimer1 >= cooldown1) {
            onCoolDown1 = false;
            cooldownTimer1 = 0;
        }

        if (cooldownTimer2 >= cooldown2) {
            onCoolDown2 = false;
            cooldownTimer2 = 0;
        }

        if (cooldownTimer3 >= cooldown3) {
            onCoolDown3 = false;
            cooldownTimer3 = 0;
        }
    }


    /*
    [Command]
    public virtual void CmdDealDamage(GameObject enemy, int ability) {
        Debug.Log("called");

    }
    */

}
