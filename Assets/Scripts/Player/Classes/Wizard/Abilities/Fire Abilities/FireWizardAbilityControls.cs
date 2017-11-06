using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireWizardAbilityControls : AbstractAbilityControls {

	FireWizardAbilities abilities;
	//private PlayerWizardStatsTest stats;

	// Use this for initialization
	void Start () {
        
		stats = GetComponent<PlayerWizardStatsTest> ();
		abilities = GetComponent<FireWizardAbilities> ();
		cooldown1 = abilities.GetFirstAbility ().GetCoolDown();
		cooldown2 = abilities.GetSecondAbility ().GetCoolDown();
        cooldown3 = abilities.GetThirdAbility().GetCoolDown();
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if(onCoolDown1 || onCoolDown2 || onCoolDown3)
			CalculateCooldown ();
		*/

        if (onCoolDown1 || onCoolDown2 || onCoolDown3)
            CalculateCooldown();
        
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
}
