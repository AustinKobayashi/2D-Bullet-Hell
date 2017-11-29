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
		
		CalculateCooldown();
        
        if (!isLocalPlayer)
            return;
        
		if(Input.GetKeyDown(KeyCode.Alpha1))
			abilities.CmdCastFirstAbility(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
		
		if(Input.GetKeyDown(KeyCode.Alpha2))
            abilities.CmdCastSecondAbility();
		
        if (Input.GetKeyDown(KeyCode.Alpha3))
            abilities.CmdCastThirdAbility(Camera.main.ScreenToWorldPoint(Input.mousePosition));   
	}
}
