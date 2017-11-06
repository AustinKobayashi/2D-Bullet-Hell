using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AbilityControls : NetworkBehaviour {

	float cooldown1;
	float cooldownTimer1;
	bool onCoolDown1;

	float cooldown2;
	float cooldownTimer2;
	bool onCoolDown2;

	float cooldown3;
	float cooldownTimer3;
	bool onCoolDown3;

	WizardAbilities abilities;
	private PlayerWizardStatsTest stats;

	// Use this for initialization
	void Start () {
		stats = GetComponent<PlayerWizardStatsTest> ();
		abilities = GetComponent<WizardAbilities> ();
		cooldown1 = abilities.GetFirstAbility ().GetCoolDown();
		cooldown2 = abilities.GetSecondAbility ().GetCoolDown();
        cooldown3 = abilities.GetThirdAbility().GetCoolDown();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(onCoolDown1 || onCoolDown2 || onCoolDown3)
			CalculateCooldown ();
		
		if(Input.GetKeyDown(KeyCode.Alpha1) && !onCoolDown1){
			onCoolDown1 = true;
			abilities.CastFirstAbility(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, this);
		}

		if(Input.GetKeyDown(KeyCode.Alpha2) && !onCoolDown2){
			onCoolDown2 = true;
			abilities.CastSecondAbility(stats);
		}

        if (Input.GetKeyDown(KeyCode.Alpha3) && !onCoolDown3){
            onCoolDown3 = true;
            abilities.CastThirdAbility(Camera.main.ScreenToWorldPoint(Input.mousePosition), this);
        }
	}


	[Server]
	void CalculateCooldown(){
		if(onCoolDown1)
			cooldownTimer1 += Time.deltaTime;
        
		if(onCoolDown2)
			cooldownTimer2 += Time.deltaTime;

        if (onCoolDown3)
            cooldownTimer3 += Time.deltaTime;
		
		if(cooldownTimer1 >= cooldown1){
			onCoolDown1 = false;
			cooldownTimer1 = 0;
		}

		if(cooldownTimer2 >= cooldown2){
			onCoolDown2 = false;
			cooldownTimer2 = 0;
		}

        if (cooldownTimer3 >= cooldown3){
            onCoolDown3 = false;
            cooldownTimer3 = 0;
        }
	}


	[Command]
    public void CmdDealDamage(GameObject enemy, int ability){

		if (!isLocalPlayer)
			return;

		EnemyStatsTest enemyStats = enemy.GetComponent<EnemyStatsTest> ();

		// did player kill the enemy
		bool kill = false;

        if (enemyStats != null)
            kill = ability == 1 ? enemyStats.TakeDamage((int)(stats.GetAbilityPower() * (0.5f + (stats.GetStrength() + new FireBall().GetDamage()) / 50f))) :
                                            enemyStats.TakeDamage((int)(stats.GetAbilityPower() * (0.5f + (stats.GetStrength() + new FireStorm().GetDamage()) / 50f)));

		if (kill)
			stats.IncreaseExperience (enemyStats.GetExperienceGain ());
	}
}
