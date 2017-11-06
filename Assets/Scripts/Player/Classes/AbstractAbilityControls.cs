using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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


    [Server]
    protected void CalculateCooldown()
    {
        if (onCoolDown1)
            cooldownTimer1 += Time.deltaTime;

        if (onCoolDown2)
            cooldownTimer2 += Time.deltaTime;

        if (onCoolDown3)
            cooldownTimer3 += Time.deltaTime;

        if (cooldownTimer1 >= cooldown1)
        {
            onCoolDown1 = false;
            cooldownTimer1 = 0;
        }

        if (cooldownTimer2 >= cooldown2)
        {
            onCoolDown2 = false;
            cooldownTimer2 = 0;
        }

        if (cooldownTimer3 >= cooldown3)
        {
            onCoolDown3 = false;
            cooldownTimer3 = 0;
        }
    }


    [Command]
    public void CmdDealDamage(GameObject enemy, int ability)
    {

        if (!isLocalPlayer)
            return;

        EnemyStatsTest enemyStats = enemy.GetComponent<EnemyStatsTest>();

        // did player kill the enemy
        bool kill = false;

        if (enemyStats != null)
            kill = ability == 1 ? enemyStats.TakeDamage((int)(stats.GetAbilityPower() * (0.5f + (stats.GetStrength() + new FireBall().GetDamage()) / 50f))) :
                                            enemyStats.TakeDamage((int)(stats.GetAbilityPower() * (0.5f + (stats.GetStrength() + new FireStorm().GetDamage()) / 50f)));

        if (kill)
            stats.IncreaseExperience(enemyStats.GetExperienceGain());
    }
}
