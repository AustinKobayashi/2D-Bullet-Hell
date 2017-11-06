using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FrostWizardAbilities : Abilities {

    public GameObject iciclePrefab;
    public GameObject fireShieldPrefab;
    public GameObject fireStormPrefab;

    // Use this for initialization
    void Awake()
    {
        firstAbility = new Icicle();
        secondAbility = new FireShield();
        thirdAbility = new FireStorm();
    }


    // Update is called once per frame
    void Update()
    {

    }


    [Server]
    public void CastFirstAbility(Vector2 target, FrostWizardAbilityControls abilityControls)
    {
        GameObject tempIcicle = Instantiate(iciclePrefab, transform.position, Quaternion.identity) as GameObject;
        tempIcicle.GetComponent<IcicleMovement>().SetTarget(target);
        tempIcicle.GetComponent<IcicleMovement>().SetAbilityControls(abilityControls);
    }

    [Server]
    public void CastSecondAbility(PlayerWizardStatsTest stats)
    {
        GameObject tempFireShield = Instantiate(fireShieldPrefab, this.transform) as GameObject;
        tempFireShield.GetComponent<FireShieldController>().SetStats(stats);
        tempFireShield.GetComponent<FireShieldController>().AddDefence((int)(stats.GetDefence() * 0.25f), 5);
    }

    [Server]
    public void CastThirdAbility(Vector2 target, FireWizardAbilityControls abilityControls)
    {
        GameObject tempFireStorm = Instantiate(fireStormPrefab, target, Quaternion.identity) as GameObject;
        tempFireStorm.GetComponent<FireStormController>().SetAbilityControls(abilityControls);
    }
}
