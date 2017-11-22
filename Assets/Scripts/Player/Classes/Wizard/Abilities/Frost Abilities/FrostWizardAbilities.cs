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
    public void CmdCastFirstAbility(Vector2 target, GameObject player) {
        if (GetComponent<AbstractAbilityControls>().onCoolDown1){ return;}
        GetComponent<AbstractAbilityControls>().onCoolDown1 = true;
        GameObject tempIcicle = Instantiate(iciclePrefab, transform.position, Quaternion.identity) as GameObject;
        tempIcicle.GetComponent<IcicleMovement>().SetTarget(target);
        tempIcicle.GetComponent<IcicleMovement>().SetAbilityControls(player.GetComponent<FrostWizardAbilityControls>());
        NetworkServer.Spawn(tempIcicle);
    }


    [Command]
    public void CmdCastSecondAbility(GameObject player){
        if (GetComponent<AbstractAbilityControls>().onCoolDown2) return;
        GetComponent<AbstractAbilityControls>().onCoolDown2 = true;
        StartCoroutine(InvulnerableDuration(player));
    }

    // TODO implement frozen status effect
    IEnumerator InvulnerableDuration(GameObject player){

        PlayerWizardStatsTest stats = player.GetComponent<PlayerWizardStatsTest>();
        GameObject tempIceBlock = Instantiate(iceBlockPrefab, transform.position, Quaternion.identity) as GameObject;
        tempIceBlock.transform.parent = transform;
        //tempIceBlock.GetComponent<IceBlockController>().parentNetId = gameObject.GetComponent<NetworkIdentity>().netId; // Set the parent network ID
        NetworkServer.Spawn(tempIceBlock);
        stats.CmdSetInvulnerable(true);
        yield return new WaitForSeconds(new IceBlock().GetDuration());
        stats.CmdSetInvulnerable(false);
        NetworkServer.Destroy(tempIceBlock);
        Destroy(tempIceBlock);
        yield return null;
    }


    // TODO attack must stun enemy (not freeze or else enemy will be invulnerable)
    // TODO should root while casting
    [Command]
    public void CmdCastThirdAbility(Vector2 target, GameObject player){
        if (GetComponent<AbstractAbilityControls>().onCoolDown3) return;
        GetComponent<AbstractAbilityControls>().onCoolDown3 = true;
        GameObject tempFrostCone = Instantiate(frostConePrefab, transform.position, Quaternion.identity) as GameObject;
        tempFrostCone.transform.parent = transform;

        var dir = target - (Vector2)transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        tempFrostCone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //tempFrostCone.GetComponent<FrostConeController>().SetDuration(new FrostCone().GetDuration());
        tempFrostCone.GetComponent<FrostConeController>().SetAbilityControls(player.GetComponent<FrostWizardAbilityControls>());
        NetworkServer.Spawn(tempFrostCone);
    }
}
