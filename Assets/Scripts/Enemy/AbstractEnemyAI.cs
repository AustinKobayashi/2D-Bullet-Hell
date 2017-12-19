using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AbstractEnemyAI : NetworkBehaviour {

    bool isExiting;
    public GameObject itemDrop;
    public EnemyMovement movement;
    public EnemyAttack attack;
    public GameObject target;


    // Use this for initialization
    void Awake() {
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttack>();
    }

    void OnApplicationQuit() {
        isExiting = true;
    }



    void OnDestroy() {
        if (isExiting) return;
        if (!isServer) return;
        Drop();
    }



    [Server]
    public void Drop() {
        var drop = Instantiate(itemDrop, transform.position, Quaternion.identity);
        var i = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>().Roll();
        drop.GetComponent<ItemDrop>().SetItem(i.GetId());
        NetworkServer.Spawn(drop);
    }
}
