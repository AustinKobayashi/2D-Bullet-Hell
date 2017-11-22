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

        // TODO check if cooldowns work on client players
        /*
        if (!isLocalPlayer)
            return;
        */

        if(onCoolDown1 || onCoolDown2 || onCoolDown3)
            CmdCalculateCooldown ();

        if (!isLocalPlayer)
            return;
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && !onCoolDown1){
            onCoolDown1 = true;
            abilities.CmdCastFirstAbility(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !onCoolDown2){
            onCoolDown2 = true;
            abilities.CmdCastSecondAbility(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && !onCoolDown3){
            onCoolDown3 = true;
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
            kill = ability == 1 ? enemyStats.TakeDamage((int)(stats.GetAbilityPower() * (0.5f + (stats.GetStrength() + new Icicle().GetDamage()) / 50f))) :
                                            enemyStats.TakeDamage((int)(stats.GetAbilityPower() * (0.5f + (stats.GetStrength() + new FrostCone().GetDamage()) / 50f)));

        if (kill)
            stats.IncreaseExperience(enemyStats.GetExperienceGain());
    }
}
