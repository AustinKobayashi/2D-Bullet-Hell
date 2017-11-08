using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// TODO implement passive
public class FireWizardAbilities : Abilities {

	public GameObject fireBallPrefab;
	public GameObject fireShieldPrefab;
    public GameObject fireStormPrefab;

	// Use this for initialization
	void Awake () {
		firstAbility = new FireBall ();
		secondAbility = new FireShield ();
        thirdAbility = new FireStorm ();
	}

	// Update is called once per frame
	void Update () {

	}

	[Command]
    public void CmdCastFirstAbility(Vector2 target, FireWizardAbilityControls abilityControls){
		GameObject tempFireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity) as GameObject;
		tempFireBall.GetComponent<FireBallMovement>().SetTarget(target);
		tempFireBall.GetComponent<FireBallMovement> ().SetAbilityControls (abilityControls);
		NetworkServer.Spawn(tempFireBall);
	}

	[Command]
	public void CmdCastSecondAbility(PlayerWizardStatsTest stats){
		GameObject tempFireShield = Instantiate (fireShieldPrefab, this.transform) as GameObject;
		tempFireShield.GetComponent<FireShieldController> ().SetStats (stats);
		tempFireShield.GetComponent<FireShieldController>().AddDefence((int)(stats.GetDefence() * 0.25f), 5);
		NetworkServer.Spawn(tempFireShield);
	}

    [Command]
    public void CmdCastThirdAbility(Vector2 target, FireWizardAbilityControls abilityControls)
    {
        GameObject tempFireStorm = Instantiate(fireStormPrefab, target, Quaternion.identity) as GameObject;
        tempFireStorm.GetComponent<FireStormController>().SetAbilityControls(abilityControls);
	    NetworkServer.Spawn(tempFireStorm);
    }
}
