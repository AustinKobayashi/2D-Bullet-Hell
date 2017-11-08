using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FrostWizardAbilities : Abilities {

    public GameObject iciclePrefab;
    public GameObject iceBlockPrefab;
    public GameObject frostConePrefab;

    // Use this for initialization
    void Awake()
    {
        firstAbility = new Icicle();
        secondAbility = new IceBlock();
        thirdAbility = new FrostCone();
    }


    // Update is called once per frame
    void Update()
    {

    }


    [Command]
    public void CmdCastFirstAbility(Vector2 target, FrostWizardAbilityControls abilityControls)
    {
        GameObject tempIcicle = Instantiate(iciclePrefab, transform.position, Quaternion.identity) as GameObject;
        tempIcicle.GetComponent<IcicleMovement>().SetTarget(target);
        tempIcicle.GetComponent<IcicleMovement>().SetAbilityControls(abilityControls);
        NetworkServer.Spawn(tempIcicle);
    }

    [Command]
    public void CmdCastSecondAbility(PlayerWizardStatsTest stats)
    {
        StartCoroutine(InvulnerableDuration(stats));
    }

    // TODO implement frozen status effect
    IEnumerator InvulnerableDuration(PlayerWizardStatsTest stats){

        GameObject tempIceBlock = Instantiate(iceBlockPrefab, transform) as GameObject;
        NetworkServer.Spawn(tempIceBlock);
        stats.CmdSetInvulnerable(true);
        yield return new WaitForSeconds(new IceBlock().GetDuration());
        stats.CmdSetInvulnerable(false);
        Destroy(tempIceBlock);
        yield return null;
    }


    // TODO attack must stun enemy (not freeze or else enemy will be invulnerable)
    [Command]
    public void CmdCastThirdAbility(Vector2 target, FrostWizardAbilityControls abilityControls)
    {
        GameObject tempFrostCone = Instantiate(frostConePrefab, transform) as GameObject;
        var dir = target - (Vector2)transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        tempFrostCone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        tempFrostCone.GetComponent<FrostConeController>().SetDuration(new FrostCone().GetDuration());
        tempFrostCone.GetComponent<FrostConeController>().SetAbilityControls(abilityControls);
        NetworkServer.Spawn(tempFrostCone);
    }
}
