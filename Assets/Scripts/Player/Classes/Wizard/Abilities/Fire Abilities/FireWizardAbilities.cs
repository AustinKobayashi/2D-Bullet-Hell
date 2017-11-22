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
    public void CmdCastFirstAbility(Vector2 target, GameObject player){
        
        FireWizardAbilityControls abilityControls = player.GetComponent<FireWizardAbilityControls>();
		GameObject tempFireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity) as GameObject;
		tempFireBall.GetComponent<FireBallMovement>().SetTarget(target);
		tempFireBall.GetComponent<FireBallMovement> ().SetAbilityControls (abilityControls);
        NetworkServer.Spawn(tempFireBall);
	}

    [Command]
    public void CmdCastSecondAbility(GameObject player){
        
        PlayerWizardStatsTest stats = player.GetComponent<PlayerWizardStatsTest>();
        GameObject tempFireShield = Instantiate(fireShieldPrefab, transform.position, Quaternion.identity) as GameObject;
        tempFireShield.transform.parent = transform;
        NetworkServer.Spawn(tempFireShield);
		tempFireShield.GetComponent<FireShieldController> ().SetStats (stats);
		tempFireShield.GetComponent<FireShieldController>().AddDefence((int)(stats.GetDefence() * 0.25f), 5);
	}

    [Command]
    public void CmdCastThirdAbility(Vector2 target, GameObject player){
        
        FireWizardAbilityControls abilityControls = player.GetComponent<FireWizardAbilityControls>();
        GameObject tempFireStorm = Instantiate(fireStormPrefab, target, Quaternion.identity) as GameObject;
        tempFireStorm.GetComponent<FireStormController>().SetAbilityControls(abilityControls);
        NetworkServer.Spawn(tempFireStorm);
    }
}
