using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// TODO implement passive
public class FireWizardAbilities : Abilities {

	public GameObject fireBallPrefab;
	public GameObject fireShieldPrefab;
    public GameObject fireStormPrefab;
	private AbstractPlayerStats stats;

	// Use this for initialization
	void Awake () {
		firstAbility = new FireBall ();
		secondAbility = new FireShield ();
        thirdAbility = new FireStorm ();
		stats = GetComponent<AbstractPlayerStats>();
	}

	// Update is called once per frame
	void Update () {

	}

    [Command]
    public void CmdCastFirstAbility(Vector2 target, GameObject player){
        
        if (GetComponent<AbstractAbilityControls>().onCoolDown1) return; 
        GetComponent<AbstractAbilityControls>().onCoolDown1 = true;

        FireWizardAbilityControls abilityControls = player.GetComponent<FireWizardAbilityControls>();
		GameObject tempFireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity) as GameObject;
		tempFireBall.GetComponent<FireBallMovement>().SetTarget(target);
		tempFireBall.GetComponent<FireBallMovement> ().SetShooter(gameObject);
	    tempFireBall.GetComponent<FireBallMovement> ().SetDamage((int)(stats.GetAbilityPower() * (0.5f + (stats.GetStrength() + new FireBall().GetDamage()) / 50f)));
        NetworkServer.Spawn(tempFireBall);
	}

    [Command]
    public void CmdCastSecondAbility(GameObject player){

        if (GetComponent<AbstractAbilityControls>().onCoolDown2) return; 
        GetComponent<AbstractAbilityControls>().onCoolDown2 = true;

        PlayerWizardStatsTest stats = player.GetComponent<PlayerWizardStatsTest>();
        GameObject tempFireShield = Instantiate(fireShieldPrefab, transform.position, Quaternion.identity) as GameObject;
        tempFireShield.transform.parent = transform;
        NetworkServer.Spawn(tempFireShield);
		tempFireShield.GetComponent<FireShieldController> ().SetStats (stats);
        tempFireShield.GetComponent<FireShieldController>().AddDefence((int)(stats.GetDefence() * 0.25f), new FireShield().GetDuration());
	}

    [Command]
    public void CmdCastThirdAbility(Vector2 target, GameObject player){
        
        if (GetComponent<AbstractAbilityControls>().onCoolDown3) return; 
        GetComponent<AbstractAbilityControls>().onCoolDown3 = true;

        FireWizardAbilityControls abilityControls = player.GetComponent<FireWizardAbilityControls>();
        GameObject tempFireStorm = Instantiate(fireStormPrefab, target, Quaternion.identity) as GameObject;
        tempFireStorm.GetComponent<FireStormController>().SetAbilityControls(abilityControls);
        NetworkServer.Spawn(tempFireStorm);
    }
}
