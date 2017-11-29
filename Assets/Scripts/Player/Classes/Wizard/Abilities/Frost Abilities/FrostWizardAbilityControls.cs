using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FrostWizardAbilityControls : AbstractAbilityControls {

    FrostWizardAbilities abilities;

    //private PlayerWizardStatsTest stats;

    // Use this for initialization

    void Start(){

        stats = GetComponent<PlayerWizardStatsTest>();
        abilities = GetComponent<FrostWizardAbilities>();
        cooldown1 = abilities.GetFirstAbility().GetCoolDown();
        cooldown2 = abilities.GetSecondAbility().GetCoolDown();
        cooldown3 = abilities.GetThirdAbility().GetCoolDown();

    }


    // Update is called once per frame
    void Update(){
        
        CalculateCooldown ();

        if (!isLocalPlayer)
            return;        

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            abilities.CmdCastFirstAbility(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            abilities.CmdCastSecondAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            abilities.CmdCastThirdAbility(Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject);
        }
    }



    [Command]
    public void CmdDealDamage(GameObject enemy, int ability){

        if (!isLocalPlayer)
            return;

        EnemyStatsTest enemyStats = enemy.GetComponent<EnemyStatsTest>();

        // did player kill the enemy
        bool kill = false;

        if (enemyStats != null)
            kill = enemyStats.TakeDamage((int)(stats.GetAbilityPower() * (0.5f + (stats.GetStrength() + new FrostCone().GetDamage()) / 50f)));

        if (kill)
            stats.IncreaseExperience(enemyStats.GetExperienceGain());
    }
}
